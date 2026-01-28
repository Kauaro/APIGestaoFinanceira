using GestaoFinanceiraApi.Entity;
using Microsoft.EntityFrameworkCore;


namespace GestaoFinanceiraApi.Data.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();



            modelBuilder.Entity<Investimento>()
                .ToTable("Investimentos", "dbo");

            modelBuilder.Entity<Investimento>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Investimento>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();
        }




        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Gastos> Gastos { get; set; }
        public DbSet<Ganhos> Ganhos { get; set; }
        public DbSet<Investimento> Investimentos { get; set; }



    }
}
