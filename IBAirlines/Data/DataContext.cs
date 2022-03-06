using IBAirlines.Models;
using Microsoft.EntityFrameworkCore;

namespace IBAirlines.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Pilote> Pilotes { get; set; }
        public DbSet<Passager> Passagers { get; set; }
        public DbSet<Avion> Avions { get; set; }
        public DbSet<Vol> Vols { get; set; }
        public DbSet<AffecteVol> AffecteVols { get; set; }
    }
}
