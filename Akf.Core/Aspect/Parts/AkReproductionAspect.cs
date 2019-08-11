using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Reflection;
using System.Text;

namespace Akf.Core.Aspect.Parts
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Akf.Core.Aspect.Parts.IAkAspectParts" />
    [Export(typeof(IAkAspectParts))]
    internal class AkReproductionAspect : IAkAspectParts
    {

        /// <summary>
        /// メソッド実行前処理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="instance"></param>
        /// <param name="args">The arguments.</param>
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

        /// <summary>
        /// Posts the process.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="instance"></param>
        /// <param name="args">The arguments.</param>
        /// <param name="result">The result.</param>
        public void PostProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args, object result)
        {
            //何もしない
        }

        /// <summary>
        /// Exceptions the process.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="instance"></param>
        /// <param name="args">The arguments.</param>
        /// <param name="ex">The ex.</param>
        public void ExceptionProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args, Exception ex)
        {
            //何もしない
        }
    }
}
