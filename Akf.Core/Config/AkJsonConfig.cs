using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Akf.Core.Config
{
    public sealed class AkJsonConfig
    {
        private static IConfigurationRoot configRoot = null;

        private static bool isInit = false;

        private AkJsonConfig()
        {
        }

        private static AkJsonConfig _Instance = null;
        public static AkJsonConfig Instance
        {
            get
            {
                if (!isInit)
                {
                    throw new ApplicationException("初期化されていません。");
                }
                return _Instance;
            }
        }

        public static void Init(string filePath)
        {
            if (_Instance == null)
            {
                _Instance = new AkJsonConfig();
            }
            isInit = true;
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(filePath, optional: true);
            //builder.AddEnvironmentVariables();
            configRoot = builder.Build();
        }

        public string GetConfig(string section, string key)
        {
            var sec = configRoot.GetSection(section);
            return sec[key];
        }

        public T GetConfig<T>(string section, string key)
        {
            var sec = configRoot.GetSection(section);
            string ret = sec[key];

            Type type = typeof(T);
            if (typeof(int) == type)
            {
                return (T)(object)int.Parse(ret);
            }
            else if (typeof(short) == type)
            {
                return (T)(object)short.Parse(ret);
            }
            else if (typeof(long) == type)
            {
                return (T)(object)long.Parse(ret);
            }
            else if (typeof(double) == type)
            {
                return (T)(object)double.Parse(ret);
            }
            else if (typeof(float) == type)
            {
                return (T)(object)float.Parse(ret);
            }
            else if (typeof(decimal) == type)
            {
                return (T)(object)decimal.Parse(ret);
            }
            else if (typeof(object) == type)
            {
                return (T)(object)ret;
            }
            else
            {
                throw new ApplicationException("対応していない型です。");
            }
        }

        public string[] GetConfigArray(string section, string key)
        {
            var sec = configRoot.GetSection(section).GetSection(key);
            return sec.GetChildren().Select(x => x.Value).ToArray();
        }

        public string GetConfig(string section, string subSection, string key)
        {
            var sec = configRoot.GetSection(section);
            var subSec = sec.GetSection(subSection);
            return subSec[key];
        }

    }
}
