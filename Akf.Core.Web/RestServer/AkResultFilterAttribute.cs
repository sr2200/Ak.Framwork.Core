using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akf.Core.Web.RestServer
{
    public class AkResultFilterAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("@@OnResultExecuting：" + this.GetType().Name) ;
            base.OnResultExecuting(context);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Console.WriteLine("@@OnResultExecutionAsync：" + this.GetType().Name);
            return base.OnResultExecutionAsync(context, next);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("@@OnResultExecuted：" + this.GetType().Name);
            base.OnResultExecuted(context);
        }
    }
}
