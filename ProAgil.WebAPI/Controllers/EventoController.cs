using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;
using System.Threading.Tasks;
using ProAgil.Domain;


namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController: ControllerBase
    {
        private readonly IProAgilRepository _proAgilRepository;

        public EventoController(IProAgilRepository proAgilRepository) 
        {
            _proAgilRepository = proAgilRepository;         
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _proAgilRepository.GetAllEventoAsync(true);
                return Ok(results);

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var results = await _proAgilRepository.GetAllEventoAsyncById(EventoId, true);
                return Ok(results);

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var results = await _proAgilRepository.GetAllEventoAsyncByTema(tema, true);
                return Ok(results);

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _proAgilRepository.Add(model);

                if (await _proAgilRepository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model}", model);                    
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Post(int eventoId, Evento model)
        {
            try
            {
                var evento = _proAgilRepository.GetAllEventoAsyncById(eventoId, false);

                if (evento == null) { return NotFound(); }


                _proAgilRepository.Update(model);

                if (await _proAgilRepository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                var evento = _proAgilRepository.GetAllEventoAsyncById(eventoId, false);

                if (evento == null) { return NotFound(); }


                _proAgilRepository.Delete(evento);

                if (await _proAgilRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            return BadRequest();
        }
    }
}