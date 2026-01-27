using Microsoft.AspNetCore.Mvc;
using Ohara.API.Application.Interfaces;

namespace Ohara.API.Internal.Controllers
{
    [ApiController]
    [Route("api/v/[controller]")]
    [Produces("application/json")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet("livros")]
        public async Task<IActionResult> Livros(Guid id)
        {
            var autor = await _autorService.LivroPorAutorAsync(id);
            if (autor == null) return NotFound("Nenhum livro encontrado!");
            return Ok(autor);
        }

        [HttpGet]
        public async Task<IActionResult> Autor(string nome)
        {
            var autorLivro = await _autorService.AutorAsync(nome);
            if (autorLivro == null) return NotFound("Nenhum autor encontrado");
            return Ok(autorLivro);
        }
    }
}
