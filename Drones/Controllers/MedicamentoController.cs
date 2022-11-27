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
                //Me ha costado trabajo encontrar una expresion regular para validar los datos de los medicamentos
                //Es algo q nunca he hecho pero estoy trabajando todavia en ello

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.validarToken(identity);
                if (!rToken.success) return rToken;
                Usuario usuario = rToken.result;
                if (usuario.rol != "administrador")
                    throw new Exception("No tienes permisos para Realizar esta accion");


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

            }catch(Exception ex)
            {
                return StatusCode(403, ex.Message);
            }
        }

    }
}
