using Drones.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drones.Models;

namespace Drones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultarBateriaController : Controller
    {
        private readonly DronAPIDbContext dbContext;
        public ConsultarBateriaController(DronAPIDbContext dbContext)
        {

            this.dbContext = dbContext;
        }

        [HttpGet("{serieNumber}")]
        public async Task<IActionResult> ComprobarBateria(string serieNumber)
        {
            try
            {
                var dronConsultar = await dbContext.Drones.Where(d => d.serieNumber==serieNumber).FirstAsync();
                if (dronConsultar == null)
                    throw new BadHttpRequestException("El codigo no se corresponde con ningun dron");


                return Ok(dronConsultar.capacidadBateria);
            }catch(Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
