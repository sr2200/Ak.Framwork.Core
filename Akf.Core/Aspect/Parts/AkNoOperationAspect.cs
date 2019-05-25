using System;
using System.Collections.Generic;
using System.Composition;
using System.Reflection;
using System.Text;

namespace Akf.Core.Aspect.Parts
{
    [Export(typeof(IAkAspectParts))]
    public class AkNoOperationAspect : IAkAspectParts
    {
        /// <summary>
        /// Pres the process.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void PreProcess(Guid id, MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine("{0} [{1}] {2}", 
                                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), 
                                targetMethod.Name, 
                                "PreProcess");
        }

        /// <summary>
        /// Posts the process.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="result">The result.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void PostProcess(Guid id, MethodInfo targetMethod, object[] args, object result)
        {
            Console.WriteLine("{0} [{1}] {2}",
                                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                targetMethod.Name,
                                "PostProcess");
        }

        /// <summary>
        /// Exceptions the process.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="ex">The ex.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ExceptionProcess(Guid id, MethodInfo targetMethod, object[] args, Exception ex)
        {
            Console.WriteLine("{0} [{1}] {2}",
                                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                targetMethod.Name,
                                "ExceptionProcess");
        }
    }
}
