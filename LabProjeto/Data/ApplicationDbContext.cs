using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LabProjeto.Models;

namespace LabProjeto.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LabProjeto.Models.JogoModel> JogoModel { get; set; }
        public DbSet<LabProjeto.Models.CategoriaModel> CategoriaModel { get; set; }
       
        public DbSet<LabProjeto.Models.PerfilModel> PerfilModel { get; set; }
        
        public DbSet<LabProjeto.Models.PerfilJogos> PerfilJogos { get; set; }
        
        
        public DbSet<LabProjeto.Models.PerfilCategoria> PerfilCategoria { get; set; }
        


    }
}