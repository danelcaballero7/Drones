using System.ComponentModel.DataAnnotations;
namespace Drones.Models
{
    public class Medicamento
    {
        public string nombre { get; set; }
        public int peso { get; set; }
        [Key]
        [StringLength(100)]
        public string codigo { get; set; }
        public string? UrlFoto { get; set; }
        public string? DronserieNumber { get; set; }    
        public bool cargado { get; set; } = false;

        
    }
}
// Nombre (permitido solo letras, números, ‘-‘, ‘_’);
// Peso;
// Código (permitido solo letra mayúscula, guión bajo y números);
// Imagen (imagen del medicamento).