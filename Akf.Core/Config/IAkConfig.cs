using System;
using System.Collections.Generic;
using System.Text;

namespace Akf.Core.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAkConfig
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string GetConfig(string section, string key);

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetConfig<T>(string section, string key);

        /// <summary>
        /// Gets the configuration array.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string[] GetConfigArray(string section, string key);

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="subSection">The sub section.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string GetConfig(string section, string subSection, string key);
    }
}
