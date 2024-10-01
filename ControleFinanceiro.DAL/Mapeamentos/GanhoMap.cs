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
    public class GanhoMap : IEntityTypeConfiguration<Ganho>
    {
        public void Configure(EntityTypeBuilder<Ganho> builder)
        {
            builder.HasKey(g => g.GanhoID);
            builder.Property(g => g.Descricao).IsRequired().HasMaxLength(50);
            builder.Property(g => g.Valor).IsRequired();
            builder.Property(g => g.Dia).IsRequired();
            builder.Property(g => g.Ano).IsRequired();
            builder.HasOne(g => g.Categoria).WithMany(g => g.Ganhos).HasForeignKey(g=>g.CategoriaID).IsRequired();
            builder.HasOne(g => g.Mes).WithMany(g => g.Ganhos).HasForeignKey(g => g.MesID).IsRequired();
            builder.HasOne(g => g.Usuario).WithMany(g => g.Ganhos).HasForeignKey(g => g.UsuarioID).IsRequired();
            builder.ToTable("Ganhos");
        }
    }
}
