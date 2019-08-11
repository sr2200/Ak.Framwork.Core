using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akf.Core.Web.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public abstract class AkBaseApiController : ControllerBase
    {
    }
}