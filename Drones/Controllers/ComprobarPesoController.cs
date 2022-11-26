using Drones.Data;
using Microsoft.AspNetCore.Mvc;
using Drones.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Drones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprobarPesoController : Controller
    {
        private readonly DronAPIDbContext dbContext;
        public ComprobarPesoController(DronAPIDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        [HttpPut("{serieNumber}")]
        public async Task<IActionResult> GetPeso(string serieNumber)
        {
            try
            {
                var dron = await dbContext.Drones.Where(d=>d.serieNumber== serieNumber).FirstAsync();  

                if (dron == null)
                    throw new BadHttpRequestException("Este dron no existe");

                if (dron.estado == "INACTIVO" || dron.estado == "CARGA ENTREGADA" || dron.estado == "REGRESANDO")
                    throw new BadHttpRequestException("Este dron no esta cargado");

                if (dron.model == "peso ligero")
                    return Ok(125 - dron.pesoLimite);
                if (dron.model == "peso medio")
                    return Ok(250 - dron.pesoLimite);
                if (dron.model == "peso crucero")
                    return Ok(375 - dron.pesoLimite);
                else 
                    return Ok(500 - dron.pesoLimite);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
