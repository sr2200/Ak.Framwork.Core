using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Reflection;
using System.Text;

namespace Akf.Core.Aspect.Parts
{
    [Export(typeof(IAkAspectParts))]
    internal class AkReproductionAspect : IAkAspectParts
    {

        public void PreProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args)
        {
            try
            {
                var data = new AkReproductionData();

                Type typ = instance.GetType();
                data.TypeName = typ.FullName;
                data.AssemblyName = typ.Assembly.FullName;
                data.MethodName = targetMethod.Name;
                data.Arguments = args;

                string fileName = Path.Combine(Environment.CurrentDirectory,
                    DateTime.Now.ToString("yyyyMMddHHmmss") + ".dat");
                Console.WriteLine("[出力ファイル名]" + fileName);

                //ファイル出力
                AkAspectUtility.SaveToBinaryFile(data, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("例外発生！！");
                Console.WriteLine(ex.ToString());
            }
        }

        public void PostProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args, object result)
        {
            //何もしない
        }

        public void ExceptionProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args, Exception ex)
        {
            //何もしない
        }
    }
}
