using System.ComponentModel.DataAnnotations;
namespace Drones.Models
{
    public class Dron
    {

        [Key]
        [StringLength(100)]
        public string serieNumber{ get; set; }
        public string model { get; set; }
        public int pesoLimite { get; set; } 
        public int capacidadBateria { get; set; }
        public string estado { get; set; }
        public List<Medicamento> listaMedicamentos = new List<Medicamento>(); 
        
    }    
}
//número de serie (máximo 100 caracteres);
// modelo(peso ligero, peso medio, peso crucero, peso pesado);
// peso límite(máximo 500gr);
// capacidad de la bateria (en porciento);
// estado(INACTIVO, CARGANDO, CARGADO, ENTREGANDO CARGA, CARGA ENTREGADA, REGRESANDO). 