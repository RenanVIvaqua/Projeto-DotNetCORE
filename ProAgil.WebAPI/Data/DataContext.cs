using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Data
{
    public class DataContext: DbContext
    {
       public DataContext(DbContextOptions<DataContext> Options):base(Options){}
       
       public DbSet<Evento> Eventos {get;set;}     

    }
}