using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CursoEFCore.Domain;

namespace CursoEFCore.Data.Configurations
{
    internal class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CodigoBarras).HasColumnType("varchar(80)").IsRequired();
            builder.Property(p => p.Descricao).HasColumnType("varchar(60)");
            builder.Property(p => p.Valor).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.TipoProduto).HasConversion<string>();
        }
    }
}
