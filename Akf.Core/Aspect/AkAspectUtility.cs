using Akf.Core.Aspect.Parts;
using Akf.Core.Config;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Akf.Core.Aspect
{
    /// <summary>
    /// 
    /// </summary>
    public class AkAspectUtility
    {
        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static T Create<T>(T instance)
        {
            return AkProxy<T>.Create(instance);
        }

        /// <summary>
        /// Gets the compose parts.
        /// </summary>
        /// <returns></returns>
        internal static List<IAkAspectParts> GetComposePartsForCurrentAssembly()
        {
            object parts = AkLocalContext.GetData(AkConstEnum.ComposeParts);
            if (parts == null)
            {
                List<IAkAspectParts> aspectPartsList = new List<IAkAspectParts>();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var asm in assemblies)
                {
                    var configuration = new ContainerConfiguration().WithAssembly(asm);
                    List<IAkAspectParts> tmpLst;
                    using (var container = configuration.CreateContainer())
                    {
                        tmpLst = new List<IAkAspectParts>(container.GetExports<IAkAspectParts>());
                        aspectPartsList.AddRange(tmpLst);
                    }
                }
                AkLocalContext.SetData(AkConstEnum.ComposeParts, aspectPartsList);
                parts = aspectPartsList;
            }
            Debug.Assert(parts is List<IAkAspectParts>, "型不正");
            return (List<IAkAspectParts>)parts;
        }

        /// <summary>
        /// Gets the compose parts for settings file.
        /// </summary>
        /// <param name="jsonFile">The json file.</param>
        /// <returns></returns>
        internal static List<IAkAspectParts> GetComposePartsForSettingsFile(string jsonFile)
        {
            object parts = AkLocalContext.GetData(AkConstEnum.ComposeParts);
            if (parts == null)
            {
                List<IAkAspectParts> aspectPartsList = new List<IAkAspectParts>();

                AkJsonConfig.Init(jsonFile);
                var conf = AkJsonConfig.Instance;
                string[] moduleArray = conf.GetConfigArray("AkAspect", "Modules");
                foreach (var item in moduleArray)
                {
                    char[] separator = { '|' };
                    string asmName = item.Split(separator)[0];
                    Assembly asm = Assembly.Load(asmName);

                    string typeName = item.Split(separator)[1];
                    IAkAspectParts obj = (IAkAspectParts)asm.CreateInstance(typeName);

                    aspectPartsList.Add(obj);
                }
                AkLocalContext.SetData(AkConstEnum.ComposeParts, aspectPartsList);
                parts = aspectPartsList;
            }
            Debug.Assert(parts is List<IAkAspectParts>, "型不正");
            return (List<IAkAspectParts>)parts;
        }
    }
}
