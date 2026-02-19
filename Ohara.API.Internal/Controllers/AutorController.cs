using Microsoft.AspNetCore.Mvc;
using Ohara.API.Application.Interfaces;

namespace Ohara.API.Internal.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de autores.
    /// Permite consulta de autores e seus respectivos livros.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        /// <summary>
        /// Construtor da controller de autores.
        /// </summary>
        /// <param name="autorService">Serviço responsável pelas regras de negócio de autores.</param>
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        /// <summary>
        /// Retorna a lista de todos os autores cadastrados.
        /// </summary>
        /// <returns>Lista de autores.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="404">Nenhum autor encontrado.</response>
        [HttpGet("listar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Listar()
        {
            var autores = await _autorService.ListarAsync();

            if (autores == null || !autores.Any())
                return NotFound("Nenhum autor encontrado.");

            return Ok(autores);
        }

        /// <summary>
        /// Retorna os livros de um autor específico pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador único (GUID) do autor.</param>
        /// <returns>Lista de livros do autor.</returns>
        /// <response code="200">Livros encontrados com sucesso.</response>
        /// <response code="404">Autor não encontrado.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Livros([FromRoute] Guid id)
        {
            var autor = await _autorService.LivroPorAutorAsync(id);

            if (autor == null)
                return NotFound("Autor não encontrado.");

            return Ok(autor);
        }

        /// <summary>
        /// Busca autor pelo nome.
        /// </summary>
        /// <param name="nome">Nome do autor para pesquisa.</param>
        /// <returns>Autor correspondente ao nome informado.</returns>
        /// <response code="200">Autor encontrado com sucesso.</response>
        /// <response code="404">Autor não encontrado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Autor([FromQuery] string nome)
        {
            var autorLivro = await _autorService.AutorAsync(nome);

            if (autorLivro == null)
                return NotFound("Autor não encontrado.");

            return Ok(autorLivro);
        }
    }
}
