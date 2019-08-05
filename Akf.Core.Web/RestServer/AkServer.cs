using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading;
using System.Threading.Tasks;

namespace Akf.Core.Web.RestServer
{
    /// <summary>
    /// 
    /// </summary>
    public class AkServer : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private IWebHost MyWebHost { get; set; }

        /// <summary>
        /// Runs the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Run(string[] args)
        {
            MyWebHost = CreateWebHostBuilder(args).Build();
            MyWebHost.RunAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if (MyWebHost != null)
            {
                var task = MyWebHost.StopAsync();
                task.Wait();

                MyWebHost.Dispose();
            }
        }

        /// <summary>
        /// Creates the web host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        protected IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<AkStartup>();

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
    }
}
