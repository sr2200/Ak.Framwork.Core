using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akf.Core.Web.RestServer
{
    public class AkExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Console.WriteLine("【例外発生】：" + context.Exception.Message);
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            //Console.WriteLine("例外発生：" + context.Exception.ToString());
            return base.OnExceptionAsync(context);
        }
    }
}
