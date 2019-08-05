using Akf.Core.Web.RestServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;

namespace Akf.Core.Web.Test
{
    [TestClass]
    public class AkServerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] args = { };
            using (AkServer server = new AkServer())
            {
                server.Run(args);

                using (var client = new HttpClient())
                {

                    var result = client.GetAsync("http://localhost:5000/api/values");
                    result.Wait();
                    var res = result.Result;
                    Console.WriteLine(res.StatusCode.GetHashCode());

                    Assert.AreEqual(res.StatusCode.GetHashCode(), 404);
                }
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] args = { };
            using (AkServer server = new AkServer())
            {
                server.Run(args);

                using (var client = new HttpClient())
                {
                    var result = client.GetAsync("http://localhost:5000/values");
                    result.Wait();
                    string actual = result.Result.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(actual);

                    Assert.AreEqual("[\"value1\",\"value2\"]", actual);
                }
            }
        }


        [TestMethod]
        public void TestMethod3()
        {
            string[] args = { };
            using (AkServer server = new AkServer())
            {
                server.Run(args);

                using (var client = new HttpClient())
                {
                    var result = client.GetAsync("http://localhost:5000/values/1");
                    var res = result.Result;
                    Console.WriteLine(res.StatusCode.GetHashCode());

                    Assert.AreEqual(res.StatusCode.GetHashCode(), 500);
                }
            }
        }

    }
}
