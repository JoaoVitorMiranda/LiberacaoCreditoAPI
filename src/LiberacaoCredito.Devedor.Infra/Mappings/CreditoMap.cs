using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LiberacaoCredito.Devedor.Domain.Models.Database;

namespace LiberacaoCredito.Devedor.Infra.Mappings
{
    public class CreditoMap : IEntityTypeConfiguration<Domain.Models.Database.Credito>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Database.Credito> builder)
        {
            builder.ToTable("Credito", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Vencimento)
                    .HasColumnType("DATETIME2")
                    .IsRequired();

            builder.Property(x => x.QtdParcelas)
           .HasColumnType("INT")
           .IsRequired();

            builder.Property(x => x.Valor)
           .HasColumnType("decimal](18, 2)")
           .IsRequired();

            builder.Property(x => x.Tipo)
           .HasColumnType("INT")
           .IsRequired();
        }
    }
}
