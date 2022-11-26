using System.ComponentModel.DataAnnotations;

namespace Drones.Models
{
    public class MedicamentoRequest
    {
        public string nombre { get; set; }
        public int peso { get; set; }
        [Key]
        [StringLength(100)]
        public string codigo { get; set; }
        public string? UrlFoto { get; set; }        
    }
}
