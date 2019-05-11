using Akf.Core.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akf.Core.Test
{
    [TestClass]
    public class AkConfigTest
    {
        [TestInitialize]
        public void init()
        {
            Console.WriteLine("init");
        }

        [TestMethod]
        public void TestMethod1()
        {
            string jsonFile = "appsettings.json";
            AkJsonConfig.Init(jsonFile);
            var conf = AkJsonConfig.Instance;
            Assert.AreEqual("hogehoge22", conf.GetConfig("section2", "data"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string jsonFile = "appsettings.json";
            AkJsonConfig.Init(jsonFile);
            var conf = AkJsonConfig.Instance;
            Assert.AreEqual("b22", conf.GetConfig("section2", "data2:1"));
        }


        [TestMethod]
        public void TestMethod3()
        {
            string jsonFile = "appsettings.json";
            AkJsonConfig.Init(jsonFile);
            var conf = AkJsonConfig.Instance;
            Assert.AreEqual("hoge3-1", conf.GetConfig("section3", "subSection1", "data"));
        }

        [TestMethod]
        public void TestMethod4()
        {
            string jsonFile = "appsettings.json";
            AkJsonConfig.Init(jsonFile);
            var conf = AkJsonConfig.Instance;
            Assert.AreEqual(10, conf.GetConfig<int>("section1", "data2"));
        }


        [TestMethod]
        public void TestMethod5()
        {
            string jsonFile = "appsettings.json";
            AkJsonConfig.Init(jsonFile);
            var conf = AkJsonConfig.Instance;
            string[] actual = conf.GetConfigArray("section2", "data2");
            Assert.AreEqual("a22", actual[0]);
            Assert.AreEqual("b22", actual[1]);
            Assert.AreEqual("c22", actual[2]);
        }
    }
}
