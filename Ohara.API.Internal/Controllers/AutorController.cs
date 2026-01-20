using Microsoft.AspNetCore.Mvc;
using Ohara.API.Application.Interfaces;

namespace Ohara.API.Internal.Controllers
{
    [ApiController]
    [Route("api/v/[controller]")]
    //controlador herda de Controller
    [Produces("application/json")]
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet("Buscar-Livro-Autor")]
        public async Task<IActionResult> Livros(Guid id)
        {
            var autor = await _autorService.LivroPorAutorAsync(id);
            return Ok(autor);
        }
        [HttpGet("Buscar-autor")]
        public async Task<IActionResult> Autor(string nome)
        {
            var autorLivro = await _autorService.AutorAsync(nome);
            return Ok(autorLivro);
        }
    }
}
