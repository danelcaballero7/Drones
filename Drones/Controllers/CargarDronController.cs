using Drones.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drones.Models;

namespace Drones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargarDronController : Controller
    {
        private readonly DronAPIDbContext dbContext;
        public CargarDronController(DronAPIDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        [HttpPut("{serieNumber}")]
        public async Task<IActionResult> CargarDron(string serieNumber)
        {
            var medicamentosPesoLigero = from medicamento in dbContext.Medicamentos where medicamento.peso < 126 && medicamento.cargado == false select medicamento;
            var medicamentosPesoMedio = from medicamento in dbContext.Medicamentos where medicamento.peso < 251 && medicamento.cargado == false select medicamento;
            var medicamentosPesoCrucero = from medicamento in dbContext.Medicamentos where medicamento.peso < 376 && medicamento.cargado == false select medicamento;
            var medicamentosPesoPesado = from medicamento in dbContext.Medicamentos where medicamento.peso < 501 && medicamento.cargado == false select medicamento;
            var dronACargar = await dbContext.Drones.Where(d => d.serieNumber == serieNumber).FirstAsync();
            try
            {               

                if (dronACargar == null)
                    throw new BadHttpRequestException("El codigo no se corresponde con ningun dron");
                
                if (dronACargar.estado == "CARGADO" || dronACargar.estado == " ENTREGANDO CARGA" || dronACargar.estado == " CARGA ENTREGADA" || dronACargar.estado == "REGRESANDO")
                    throw new BadHttpRequestException("Este dron no esta disponible para cargar");

                if (dronACargar.capacidadBateria < 25)
                    throw new BadHttpRequestException("El dron posee menos del 25% de la bateria. No puede cargar medicamentos ");


                dronACargar.estado = "CARGANDO";
                await dbContext.SaveChangesAsync();

                if (dronACargar.model == "peso ligero")
                {
                    foreach (var medicamento in medicamentosPesoLigero)
                    {
                        if (dronACargar.pesoLimite > 0)
                        {
                            if (dronACargar.pesoLimite - medicamento.peso > -1)
                            {
                                medicamento.DronserieNumber = dronACargar.serieNumber;
                                dronACargar.listaMedicamentos.Add(medicamento);
                                dronACargar.pesoLimite -= medicamento.peso;
                                medicamento.cargado = true;
                                await dbContext.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            dronACargar.estado = "CARGADO";

                        }

                    }
                    return Ok();
                }


                if (dronACargar.model == "peso medio")
                {
                    foreach (var medicamento in medicamentosPesoMedio)
                    {
                        if (dronACargar.pesoLimite > 0)
                        {
                            if (dronACargar.pesoLimite - medicamento.peso > -1)
                            {
                                medicamento.DronserieNumber = dronACargar.serieNumber;
                                dronACargar.listaMedicamentos.Add(medicamento);
                                dronACargar.pesoLimite -= medicamento.peso;
                                medicamento.cargado = true;
                                await dbContext.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            dronACargar.estado = "CARGADO";

                        }

                    }
                    return Ok();
                }


                if (dronACargar.model == "peso crucero")
                {
                    foreach (var medicamento in medicamentosPesoCrucero)
                    {
                        if (dronACargar.pesoLimite > 0)
                        {
                            if (dronACargar.pesoLimite - medicamento.peso > -1)
                            {
                                medicamento.DronserieNumber = dronACargar.serieNumber;
                                dronACargar.listaMedicamentos.Add(medicamento);
                                dronACargar.pesoLimite -= medicamento.peso;
                                medicamento.cargado = true;
                                await dbContext.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            dronACargar.estado = "CARGADO";

                        }

                    }
                    return Ok();
                }


                else
                {
                    foreach (var medicamento in medicamentosPesoPesado)
                    {
                        if (dronACargar.pesoLimite > 0)
                        {
                            if (dronACargar.pesoLimite - medicamento.peso > -1)
                            {
                                medicamento.DronserieNumber = dronACargar.serieNumber;
                                dronACargar.listaMedicamentos.Add(medicamento);
                                dronACargar.pesoLimite -= medicamento.peso;
                                medicamento.cargado = true;
                                await dbContext.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            dronACargar.estado = "CARGADO";

                        }

                    }
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
