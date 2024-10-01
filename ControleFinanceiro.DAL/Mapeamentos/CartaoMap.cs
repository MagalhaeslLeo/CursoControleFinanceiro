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
    public class CartaoMap : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            builder.HasKey(c => c.CartaoID);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(20);
            builder.HasIndex(c => c.Nome).IsUnique();
            builder.Property(c=>c.Bandeira).IsRequired().HasMaxLength(15);
            builder.Property(c=>c.Numero).IsRequired().HasMaxLength(30);
            builder.HasIndex(c => c.Numero).IsUnique();
            builder.Property(c=>c.Limite).IsRequired();
            //No caso de o usuário ser deletado nada vai acontecer
            builder.HasOne(c =>c.Usuario).WithMany(c=>c.Cartoes).IsRequired().HasForeignKey(c=>c.UsuarioID).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(c => c.Despesas).WithOne(c => c.Cartao);
            builder.ToTable("Cartoes");
        }
    }
}
