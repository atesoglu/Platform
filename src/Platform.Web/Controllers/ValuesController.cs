using Microsoft.AspNetCore.Mvc;
using System;

namespace Platform.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult OnError()
        {
            throw new Exception("Catch me if you can");
        }
    }
}