using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("VARCHAR(60)");
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.TipoProduto).HasConversion<string>();
        }
    }
}