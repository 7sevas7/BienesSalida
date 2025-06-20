using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BienesSalida.ConexionesBD;
using BienesSalida.Share.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


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
                await bC_Sistema.usuariosAsync(consultas.idEmp, consultas.nameEmp, "Usuario");
                var tokken = generateToken(consultas.nameEmp, consultas.idEmp.ToString());
                return Ok(new { 
                    idUser = consultas.idEmp,
                    usuario = consultas.nameEmp,
                    token = tokken
                });
            }
            else {                
                return Unauthorized(new { resultado});
            }
        
        }
        public string generateToken(string nameCurp, string idUser)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9L''e[jZ$ZVy).G:#/@?`Ig3ZYmkd^X\""));
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("9L''e[jZ$ZVy).G:#/@?`Ig3ZYmkd^X\"");
            var TokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, nameCurp),
                        new Claim(ClaimTypes.PrimarySid, idUser)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(TokenDes);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet]
        [Route("verificate")]
        [Authorize]
        public IActionResult verificateToken() {
            return Ok();
        }
        
    }
}
