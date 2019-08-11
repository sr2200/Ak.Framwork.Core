using Akf.Core.Aspect;
using Akf.Core.Aspect.Parts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Akf.Core.Test.Aspect
{
    [Export(typeof(IAkAspectParts))]
    public class AkTestAspect2 : IAkAspectParts
    {
        public void PreProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args)
        {

        }

        public void PostProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args, object result)
        {

        }

        public void ExceptionProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args, Exception ex)
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
                    "Exception_" + 
                    DateTime.Now.ToString("yyyyMMddHHmmss") + ".dat");
                Console.WriteLine("[出力ファイル名]" + fileName);



                //ファイル出力
                AkAspectUtility.SaveToBinaryFile(data, fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("例外発生！！");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
