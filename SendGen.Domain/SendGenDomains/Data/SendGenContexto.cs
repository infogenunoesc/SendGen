using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace SendGen.Domain.SendGenDomains.Data
{
    public class SendGenContexto : DbContext
    {
        public SendGenContexto()
        {
        }
        public SendGenContexto(DbContextOptions<SendGenContexto> options) : base(options)
        {
        }

        //referencia a classe Cliente - Demais classe nessitam estar aqui também
        public DbSet<Cliente> Cliente { get; set; }
		public DbSet<FiltroDB> FiltroDB { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=DESKTOP-H95BSF0\\SQLEXPRESS; Database=SendGen; Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Cliente>().HasKey(c => c.ClienteId);
			modelBuilder.Entity<FiltroDB>().HasKey(c => c.ID);
		}
	}
}

