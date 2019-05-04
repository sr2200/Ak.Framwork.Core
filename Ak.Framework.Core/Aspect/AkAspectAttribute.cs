using System;
using System.Collections.Generic;
using System.Text;

namespace Ak.Framework.Core.Aspect
{
    public class AkAspectAttribute : Attribute
    {
        public bool IsInjection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AkAspectAttribute()
        {
            IsInjection = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isExe"></param>
        public AkAspectAttribute(bool isExe)
        {
            IsInjection = isExe;
        }
    }
}
