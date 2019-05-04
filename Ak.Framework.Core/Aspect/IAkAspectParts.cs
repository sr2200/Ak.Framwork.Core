using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ak.Framework.Core.Aspect
{
    public interface IAkAspectParts
    {
        void PreProcess(Guid id, MethodInfo targetMethod, object[] args);
        void PostProcess(Guid id, MethodInfo targetMethod, object[] args, object result);
        void ExceptionProcess(Guid id, MethodInfo targetMethod, object[] args, Exception ex);
    }
}
