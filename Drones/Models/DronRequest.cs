using System.ComponentModel.DataAnnotations;

namespace Drones.Models
{
    public class DronRequest
    {
        [Key]
        [StringLength(100)]
        public string serieNumber { get; set; }
        public string model { get; set; }
    }
}
