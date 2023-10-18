using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost; Database=SendGen; Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasKey(c => c.ClienteId);
        }
    }
}
