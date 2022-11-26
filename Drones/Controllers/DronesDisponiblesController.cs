using Drones.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drones.Models;

namespace Drones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DronesDisponiblesController : Controller
    {
        private readonly DronAPIDbContext dbContext;
        public DronesDisponiblesController(DronAPIDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetDronesDisponibles()
        {            
            return Ok(await dbContext.Drones.Where(d => d.estado == "INACTIVO" || d.estado == "CARGANDO").ToListAsync());

        }
    }
}
