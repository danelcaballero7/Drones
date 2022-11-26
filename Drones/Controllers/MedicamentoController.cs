using Microsoft.AspNetCore.Mvc;
using Drones.Data;
using Drones.Models;
using Microsoft.EntityFrameworkCore;



namespace Drones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentoController:Controller
    {
        private readonly DronAPIDbContext dbContext;
        public MedicamentoController(DronAPIDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetMedicamentos()
        {
            return Ok(await dbContext.Medicamentos.Where(m => m.cargado==false).ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddMedicamento(MedicamentoRequest medicamento)
        {
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

    }
}
