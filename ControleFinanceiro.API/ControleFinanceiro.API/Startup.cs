using ControleFinanceiro.API.Extensions;
using ControleFinanceiro.API.Validacoes;
using ControleFinanceiro.API.ViewModels;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.DAL.Repositorios;
using ControleFInanceiro.BLL.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores<Contexto>();
      services.ConfigurarSenhaUsuario();

      services.AddDbContext<Contexto>(opcoes => opcoes.UseSqlServer(Configuration.GetConnectionString("ConexaoBD")));

      var key = Encoding.ASCII.GetBytes(Settings.ChaveSecreta);

      services.AddAuthentication(opcoes =>
      {
        opcoes.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opcoes.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
        .AddJwtBearer(opcoes =>
        {
          opcoes.RequireHttpsMetadata = false;
          opcoes.SaveToken = true;
          opcoes.TokenValidationParameters = new TokenValidationParameters
          {
            //Significa que esse token vai ser validado
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            //Não validar destino nem origem
            ValidateIssuer = false,
            ValidateAudience = false
          };
        });


      services.AddControllers()
          .AddJsonOptions(opcoes =>
      {
        opcoes.JsonSerializerOptions.IgnoreNullValues = true;
      })
                         .AddNewtonsoftJson(opcoes =>
                         {
                           opcoes.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                         });
      services.AddCors();

      services.AddFluentValidationAutoValidation();

      services.AddValidatorsFromAssemblyContaining<Startup>();

      services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
      services.AddScoped<ITipoRepositorio, TipoRepositorio>();
      services.AddScoped<IFuncaoRepositorio, FuncaoRepositorio>();
      services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
      services.AddScoped<ICartaoRepositorio, CartaoRepositorio>();
      services.AddScoped<IDespesaRepositorio, DespesaRepositorio>();

      services.AddTransient<IValidator<Categoria>, CategoriaValidator>();
      services.AddTransient<IValidator<Cartao>, CartaoValidator>();
      services.AddTransient<IValidator<FuncoesViewModel>, FuncoesViewModelValidator>();
      services.AddTransient<IValidator<RegistroViewModel>, RegistroViewModelValidator>();
      services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();
      services.AddTransient<IValidator<Despesa>, DespesaValidator>();

      services.AddSpaStaticFiles(diretorio =>
      {
        diretorio.RootPath = "ControleFinanceiro-UI";
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

      app.UseSpaStaticFiles();

      app.UseStaticFiles();

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "ControleFinanceiro-UI");

        if (env.IsDevelopment())
        {
          spa.UseProxyToSpaDevelopmentServer($"http://localhost:4200/");
        }
      });
    }
  }
}
