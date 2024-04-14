using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalR_gRPC_Innovations.Controllers
{
    [Route("api/[controller]")]
    [OutputCache]
    public class OutputCachingController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(DateTime.Now);
        }
        [HttpGet("GetBase")]
        [OutputCache(PolicyName="Custom")]
        public async Task<IActionResult> GetBase()
        {
            return Ok(DateTime.Now);
        }
    }
}

