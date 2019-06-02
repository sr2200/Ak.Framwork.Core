using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Akf.Core.Aspect.Parts
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AkReproductionData
    {
        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        /// <value>
        /// The name of the assembly.
        /// </value>
        public string AssemblyName { get; set; }

        /// <summary>
        /// Gets or sets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the target method information.
        /// </summary>
        /// <value>
        /// The target method information.
        /// </value>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public object[] Arguments { get; set; }
    }
}
