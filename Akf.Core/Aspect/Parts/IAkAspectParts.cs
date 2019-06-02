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
        /// <param name="instance"></param>
        /// <param name="args">The arguments.</param>
        void PreProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args);

        /// <summary>
        /// Posts the process.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="instance"></param>
        /// <param name="args">The arguments.</param>
        /// <param name="result">The result.</param>
        void PostProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args, object result);

        /// <summary>
        /// Exceptions the process.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="instance"></param>
        /// <param name="args">The arguments.</param>
        /// <param name="ex">The ex.</param>
        void ExceptionProcess<T>(Guid id, MethodInfo targetMethod, T instance, object[] args, Exception ex);
    }
}
