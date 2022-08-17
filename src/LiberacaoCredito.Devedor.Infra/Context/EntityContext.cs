using LiberacaoCredito.Devedor.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LiberacaoCredito.Devedor.Infra.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext() { }
        public EntityContext(DbContextOptions<EntityContext> options)
             : base(options) { }

        public DbSet<Domain.Models.Database.Credito> Credito { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CreditoMap());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entity => entity.Entity.GetType().GetProperty("DateCreated") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateCreated").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateCreated").IsModified = false;
                }
            }

            return base.SaveChanges();
        }
    }
}
