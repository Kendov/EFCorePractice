using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(x => x.Telefone).HasColumnType("CHAR(11)");
            builder.Property(x => x.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(x => x.Estado).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(x => x.Cidade).HasMaxLength(60).IsRequired();

            builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
        }
    }
}