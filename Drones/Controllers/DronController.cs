using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Drones.Data;
using Drones.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;



namespace Drones.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class DronController : Controller
    {
        private readonly DronAPIDbContext dbContext;
        public DronController(DronAPIDbContext dbContext)
        {

            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetDrones()
        {
            return Ok(await dbContext.Drones.ToListAsync());

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDron(DronRequest dron)
        {
            try
            {
                
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.validarToken(identity);
                if (!rToken.success) return rToken;
                Usuario usuario = rToken.result;
                if (usuario.rol != "administrador")
                    throw new Exception("No tienes permisos para Realizar esta accion");

                if(dron.serieNumber.Length> 100)
                    throw new BadHttpRequestException("El Numero de serie posee mas de 100 caracteres");

                if (dron.model == "peso ligero")
                {
                    var nuevodron = new Dron()
                    {
                        serieNumber = dron.serieNumber,
                        model = dron.model,
                        pesoLimite = 125,
                        capacidadBateria = 100,
                        estado = "INACTIVO"
                    };
                    await dbContext.Drones.AddAsync(nuevodron);
                    await dbContext.SaveChangesAsync();

                    return Ok(nuevodron);
                }
                if (dron.model == "peso medio")
                {
                    var nuevodron = new Dron()
                    {
                        serieNumber = dron.serieNumber,
                        model = dron.model,
                        pesoLimite = 250,
                        capacidadBateria = 100,
                        estado = "INACTIVO"
                    };
                    await dbContext.Drones.AddAsync(nuevodron);
                    await dbContext.SaveChangesAsync();

                    return Ok(nuevodron);
                }
                if (dron.model == "peso crucero")
                {
                    var nuevodron = new Dron()
                    {
                        serieNumber = dron.serieNumber,
                        model = dron.model,
                        pesoLimite = 375,
                        capacidadBateria = 100,
                        estado = "INACTIVO"
                    };
                    await dbContext.Drones.AddAsync(nuevodron);
                    await dbContext.SaveChangesAsync();

                    return Ok(nuevodron);
                }
                if (dron.model == "peso pesado")
                {
                    var nuevodron = new Dron()
                    {
                        serieNumber = dron.serieNumber,
                        model = dron.model,
                        pesoLimite = 500,
                        capacidadBateria = 100,
                        estado = "INACTIVO"
                    };
                    await dbContext.Drones.AddAsync(nuevodron);
                    await dbContext.SaveChangesAsync();

                    return Ok(nuevodron);
                }
                else
                {
                    throw new BadHttpRequestException("El modelo del dron solo debe ser: peso ligero, peso medio, peso crucero, peso pesado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }          
           
        }
        
    }
}
