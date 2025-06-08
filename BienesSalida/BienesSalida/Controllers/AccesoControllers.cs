using BienesSalida.ConexionesBD;
using BienesSalida.Share.Models;
using Microsoft.AspNetCore.Mvc;

namespace BienesSalida.Controllers
{
    [ApiController]
    [Route("api/acceso")]
    public class AccesoControllers:Controller
    {
        private BC_Usuarios consultas { set; get; } = new();
        private BC_SistemaBienes bC_Sistema { set; get; } = new();

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> setLogin([FromForm] UsuarioDto user) {

            var resultado = await consultas.consultaAsync(user.nombre,user.password);

            if (resultado)
            {
               // await bC_Sistema.usuariosAsync(consultas.idEmp, consultas.nameEmp, "Usuario");
                return Ok(new { resultado });
            }
            else {
                
                return Unauthorized(new { resultado});
            }
        
        }
    }
}
