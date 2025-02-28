using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilContext: DbContext
    {
       public ProAgilContext(DbContextOptions<ProAgilContext> Options):base(Options){}
       
       public DbSet<Evento> Eventos {get;set;}     

       public DbSet<Palestrante> Palestrantes {get;set;}  

       public DbSet<PalestranteEvento> PalestranteEventos {get;set;}  

       public DbSet<Lote> Lotes {get;set;}  

       public DbSet<RedeSocial> RedeSociais {get;set;}  

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new {PE.EventoId, PE.PalestranteId});
       }
    }
}