using ControleFInanceiro.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Mapeamentos
{
    public class TipoMap : IEntityTypeConfiguration<Tipo>
    {
        public void Configure(EntityTypeBuilder<Tipo> builder)
        {
            builder.HasKey(t => t.TipoID);
            builder.Property(t => t.Nome).IsRequired().HasMaxLength(20);
            //Um tipo tem muitas categorias, uma categoria tem um tipo
            builder.HasMany(t => t.Categorias).WithOne(t => t.Tipo);
            //Já vai ter dados quando eu criar o banco
            builder.HasData(
                new Tipo
                {
                    TipoID = 1,
                    Nome = "Despesa"
                },
                new Tipo
                {
                    TipoID = 2,
                    Nome = "Ganho"
                }
                );
            //Nome da tabela no banco
            builder.ToTable("Tipos");
        }
    }
}
