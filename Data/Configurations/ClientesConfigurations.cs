using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CursoEFCore.Domain;
namespace CursoEFCore.Data.Configurations
{
    internal class ClientesConfigurations : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).HasColumnType("varchar(80)").IsRequired();
            builder.Property(c => c.Telefone).HasColumnType("char(11)").IsRequired();
            builder.Property(c => c.CEP).HasColumnType("char(8)").IsRequired();
            builder.Property(c => c.Estado).HasColumnType("char(2)").IsRequired();
            builder.Property(c => c.Cidade).HasMaxLength(60).IsRequired();

            builder.HasIndex(c => c.Telefone).HasName("idx_clientes_telefone"); ;
        }
    }
}
