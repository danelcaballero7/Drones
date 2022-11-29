using Microsoft.AspNetCore.Mvc;
using Drones.Data;
using Drones.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Drones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentoController : Controller
    {
        private readonly DronAPIDbContext dbContext;
        public MedicamentoController(DronAPIDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetMedicamentos()
        {
            return Ok(await dbContext.Medicamentos.Where(m => m.cargado == false).ToListAsync());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMedicamento(MedicamentoRequest medicamento)
        {
            try
            {

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.validarToken(identity);

                //validacion de token
                if (!rToken.success) return rToken;
                Usuario usuario = rToken.result;

                //validacion de rol
                if (usuario.rol != "administrador")
                    throw new Exception("No tienes permisos para Realizar esta accion");

                //Validacion de nombre
                char[] valoresPermitidosNombre = new char[64] { '0', '1', '2','3','4','5','6', '7', '8', '9', '-', '_','q','Q',
                'w','W','e','E','r','R','t','T','y','Y','u','U','i','I','o','O','p','P','a','A','s','S','d','D','f','F',
                'g','G','h','H','j','J','k','K','l','L','z','Z','x','X','c','C','v','V','b','B','n','N','m','M'};
                char[] charsNombre = medicamento.nombre.ToCharArray();
                int nombreValido = 0;
                for (int y = 0; y < charsNombre.Length; y++)
                {
                    for (int i = 0; i < valoresPermitidosNombre.Length; i++)
                    {
                        if (charsNombre[y] == valoresPermitidosNombre[i])
                        {
                            nombreValido++;
                        }
                    }
                }
                if (nombreValido != charsNombre.Length)
                    throw new Exception("El nombre no es valido, solo puede contener letras, numeros, '-', '_'");

                //Validacion de codigo
                char[] valoresPermitidosCodigo = new char[37] { '0', '1', '2','3','4','5','6','7', '8','9','_','Q','W','E','R','T',
                'Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M'};
                char[] charsCodigo = medicamento.codigo.ToCharArray();
                int codigoValido = 0;
                for (int y = 0; y < charsCodigo.Length; y++)
                {
                    for (int i = 0; i < valoresPermitidosCodigo.Length; i++)
                    {
                        if (charsCodigo[y] == valoresPermitidosCodigo[i])
                        {
                            codigoValido++;
                        }
                    }
                }
                if (codigoValido != charsCodigo.Length)
                    throw new Exception("El codigo no es valido, solo puede contener numeros, '_' y letra mayusculas");


                //crear medicamento
                var nuevoMedicamento = new Medicamento()
                {
                    nombre = medicamento.nombre,
                    peso = medicamento.peso,
                    codigo = medicamento.codigo,
                    UrlFoto = medicamento.UrlFoto,
                    cargado = false

                };
                await dbContext.Medicamentos.AddAsync(nuevoMedicamento);
                await dbContext.SaveChangesAsync();
                return Ok(nuevoMedicamento);

            }
            catch (Exception ex)
            {
                return StatusCode(403, ex.Message);
            }
        }

    }
}
