using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Akf.Core.Web.RestServer;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Akf.Core.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (AkServer server = new AkServer())
            {
                server.Run(args);
            }
        }
    }
}
