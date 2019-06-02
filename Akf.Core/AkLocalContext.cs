using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Akf.Core
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AkLocalContext
    {
        /// <summary>
        /// The state
        /// </summary>
        internal static ConcurrentDictionary<string, AsyncLocal<object>> state =
            new ConcurrentDictionary<string, AsyncLocal<object>>();

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="data">The data.</param>
        internal static void SetData(string name, object data) =>
            state.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="data">The data.</param>
        internal static void SetData(AkConstEnum name, object data)
        {
            string nameStr = Enum.GetName(typeof(AkConstEnum), name);
            SetData(nameStr, data);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static object GetData(string name) =>
            state.TryGetValue(name, out AsyncLocal<object> data) ? data.Value : null;

        internal static object GetData(AkConstEnum name)
        {
            string nameStr = Enum.GetName(typeof(AkConstEnum), name);
            return GetData(nameStr);
        }
    }
}
