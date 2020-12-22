using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCAD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public string Trim()
        {
            return "sdfsdfsdf";
        }

        [EnableCors("MyPolicy")]
        [HttpGet]
        [Route("oauth")]
        [Authorize]
        public ActionResult Authorize()
        {
            return Redirect("https://translate.google.com.ua/");
        }
    }
}
