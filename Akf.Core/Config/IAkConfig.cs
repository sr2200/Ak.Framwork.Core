using System;
using System.Collections.Generic;
using System.Text;

namespace Akf.Core.Config
{
    public interface IAkConfig
    {
        string GetConfig(string section, string key);

        T GetConfig<T>(string section, string key);

        string[] GetConfigArray(string section, string key);

        string GetConfig(string section, string subSection, string key);
    }
}
