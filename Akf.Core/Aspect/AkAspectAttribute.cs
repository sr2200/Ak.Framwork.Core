using System;
using System.Collections.Generic;
using System.Text;

namespace Akf.Core.Aspect
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class AkAspectAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is injection.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is injection; otherwise, <c>false</c>.
        /// </value>
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
