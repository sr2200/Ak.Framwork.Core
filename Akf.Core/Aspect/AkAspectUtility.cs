using Akf.Core.Aspect.Parts;
using Akf.Core.Config;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
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

        /////// <summary>
        /////// CurrentDomainのAssemblyからAspectクラスを取得し、Cacheします。
        /////// </summary>
        /////// <returns>Aspectインスタンス一覧</returns>
        ////[Obsolete]
        ////internal static List<IAkAspectParts> GetComposePartsForCurrentAssembly()
        ////{
        ////    object parts = AkLocalContext.GetData(AkConstEnum.ComposeParts);
        ////    if (parts == null)
        ////    {
        ////        List<IAkAspectParts> aspectPartsList = new List<IAkAspectParts>();
        ////        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        ////        foreach (var asm in assemblies)
        ////        {
        ////            var configuration = new ContainerConfiguration().WithAssembly(asm);
        ////            List<IAkAspectParts> tmpLst;
        ////            using (var container = configuration.CreateContainer())
        ////            {
        ////                tmpLst = new List<IAkAspectParts>(container.GetExports<IAkAspectParts>());
        ////                aspectPartsList.AddRange(tmpLst);
        ////            }
        ////        }
        ////        AkLocalContext.SetData(AkConstEnum.ComposeParts, aspectPartsList);
        ////        parts = aspectPartsList;
        ////    }
        ////    Debug.Assert(parts is List<IAkAspectParts>, "型不正");
        ////    return (List<IAkAspectParts>)parts;
        ////}

        /// <summary>
        /// 設定ファイルからAspectクラスを取得し、Cacheします。
        /// </summary>
        /// <param name="jsonFile">The json file.</param>
        /// <returns>Aspectインスタンス一覧</returns>
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

        /// <summary>
        /// Saves to binary file.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="filePath">The file path.</param>
        public static void SaveToBinaryFile(object obj, string filePath)
        {
            using (var fs = new FileStream(filePath,
                                    FileMode.Create,
                                    FileAccess.Write))
            {
                var bf = new BinaryFormatter();
                //シリアル化して書き込む
                bf.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// Loads from binary file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static object LoadFromBinaryFile(string path)
        {
            FileStream fs = new FileStream(path,
                FileMode.Open,
                FileAccess.Read);
            BinaryFormatter f = new BinaryFormatter();
            //読み込んで逆シリアル化する
            object obj = f.Deserialize(fs);
            fs.Close();

            return obj;
        }
    }
}
