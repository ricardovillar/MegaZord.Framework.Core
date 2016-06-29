using Microsoft.EntityFrameworkCore;
namespace MegaZord.Framework.Models {
    public class MZDbContext : DbContext {
        public MZDbContext() {
        }

        public MZDbContext(DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
