using Akf.Core.Aspect.Parts;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Diagnostics;
using System.Text;

namespace Akf.Core.Aspect
{
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
        internal static List<IAkAspectParts> GetComposeParts()
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

        public static void Hoge()
        {
            AkLocalContext.SetData("", "");
            AkLocalContext.GetData("");
        }

    }
}
