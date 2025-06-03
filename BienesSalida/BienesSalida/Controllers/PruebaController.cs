using Microsoft.AspNetCore.Mvc;

namespace BienesSalida.Controllers
{
    [Route("api/pruebas")]
    public class PruebaController : Controller
    {
        [HttpGet]
        [Route("all")]
        public IActionResult Gets() {
            return Ok(new { Msg= "hello"});
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
