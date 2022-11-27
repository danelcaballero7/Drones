using Microsoft.EntityFrameworkCore;
using Drones.Models;

namespace Drones.Data
{
    public class DronAPIDbContext: DbContext
    {
        public DronAPIDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Dron> Drones { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        

    }
}
