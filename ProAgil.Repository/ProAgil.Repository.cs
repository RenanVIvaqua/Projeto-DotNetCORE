using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;


namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }       

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

         public async Task<bool> SaveChangesAsync()
        {
            return(await _context.SaveChangesAsync()) > 0;
        }

        #region Evento

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lote).Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(PE => PE.PalestrantesEventos).ThenInclude(P => P.Palestrante);        
            }

            query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

         public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes = false)
        {
           IQueryable<Evento> query = _context.Eventos.Include(c => c.Lote).Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(PE => PE.PalestrantesEventos).ThenInclude(P => P.Palestrante);        
            }

            query = query.OrderByDescending(c => c.DataEvento).Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lote).Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(PE => PE.PalestrantesEventos).ThenInclude(P => P.Palestrante);        
            }

            query = query.OrderByDescending(c => c.DataEvento).Where(c => c.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }       

        #endregion

        #region Palestrante

        public async Task<Palestrante> GetAllPalestrantesAsync(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(PE => PE.PalestrantesEventos).ThenInclude(E => E.Evento);        
            }

            query = query.OrderBy(c => c.Nome).Where(P => P.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name ,bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(PE => PE.PalestrantesEventos).ThenInclude(E => E.Evento);        
            }

            query = query.Where(P => P.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }    

        #endregion
    }
}