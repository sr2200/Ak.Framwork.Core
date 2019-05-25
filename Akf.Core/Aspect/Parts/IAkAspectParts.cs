using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Akf.Core.Aspect.Parts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAkAspectParts
    {
        /// <summary>
        /// Pres the process.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="args">The arguments.</param>
        void PreProcess(Guid id, MethodInfo targetMethod, object[] args);

        /// <summary>
        /// Posts the process.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="result">The result.</param>
        void PostProcess(Guid id, MethodInfo targetMethod, object[] args, object result);

        /// <summary>
        /// Exceptions the process.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="ex">The ex.</param>
        void ExceptionProcess(Guid id, MethodInfo targetMethod, object[] args, Exception ex);
    }
}
