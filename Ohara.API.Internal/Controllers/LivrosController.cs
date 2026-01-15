using Microsoft.AspNetCore.Mvc;
using Ohara.API.Application.Interfaces;
using Ohara.API.Application.Requests;
using Ohara.API.Application.Responses;

namespace Ohara.API.Internal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;
        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public async Task<AdicionarLivroResponse> CadastrarLivro(AdicionarLivroRequest adicionarLivroRequest)
        {
            var autor = await _livroService.AdicionarLivroAsync(adicionarLivroRequest);
            return autor;
        }
    }
}