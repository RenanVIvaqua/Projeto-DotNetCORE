using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using System.Threading.Tasks;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalestranteController : ControllerBase
    {
        private IProAgilRepository _proAgilRepository;

        public PalestranteController(IProAgilRepository proAgilRepository)
        {
            _proAgilRepository = proAgilRepository;
        }

        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> GetPalestrante(int palestranteId, bool IncludeEventos)
        {
            try
            {
                var result = await _proAgilRepository.GetAllPalestrantesAsync(palestranteId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetPalestranteByName(string name, bool IncludeEventos)
        {
            try
            {
                var result = await _proAgilRepository.GetAllPalestrantesAsyncByName(name, IncludeEventos);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _proAgilRepository.Add(model);

                if (await _proAgilRepository.SaveChangesAsync())
                {
                    return Created($"/api/Palestrante/{model}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Post(int palestranteId, Palestrante model)
        {
            try
            {
                var result = await _proAgilRepository.GetAllPalestrantesAsync(palestranteId, false);

                if (result == null) { return NotFound(); }

                _proAgilRepository.Update(model);

                if (await _proAgilRepository.SaveChangesAsync())
                {
                    return Created($"/api/Palestrante/{model}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int palestranteId)
        {
            try
            {
                var result = await _proAgilRepository.GetAllPalestrantesAsync(palestranteId, false);

                if (result == null) { return NotFound(); }

                _proAgilRepository.Delete(result);

                if (await _proAgilRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            return BadRequest();
        }
    }
}