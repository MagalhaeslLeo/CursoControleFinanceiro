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
    public class FuncaoMap : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Property(f => f.Descricao).HasMaxLength(50).IsRequired();
            builder.HasData(
                new Funcao
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrador",
                    //Função usa para comparar valores
                    NormalizedName = "ADMINISTRADOR",
                    Descricao = "Administrador do sistema"
                },
                new Funcao
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Usuario",
                    //Função usa para comparar valores
                    NormalizedName = "USUARIO",
                    Descricao = "Usuario do sistema"
                });
            builder.ToTable("Funcoes");
        }
    }
}
