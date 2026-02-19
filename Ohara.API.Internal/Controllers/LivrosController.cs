using Microsoft.AspNetCore.Mvc;
using Ohara.API.Application.Interfaces;
using Ohara.API.Shared.Enums;
using Ohara.API.Shared.Requests;

namespace Ohara.API.Internal.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de livros.
    /// Permite operações de consulta, cadastro, atualização e remoção.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        /// <summary>
        /// Construtor da controller de livros.
        /// </summary>
        /// <param name="livroService">Serviço responsável pelas regras de negócio de livros.</param>
        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        /// <summary>
        /// Busca um livro pelo seu identificador único (GUID).
        /// </summary>
        /// <param name="id">Identificador do livro.</param>
        /// <returns>Livro encontrado.</returns>
        /// <response code="200">Livro encontrado com sucesso.</response>
        /// <response code="404">Livro não encontrado.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarLivro(Guid id)
        {
            var livro = await _livroService.BuscarLivroAsync(id);

            if (livro == null)
                return NotFound("Livro não encontrado.");

            return Ok(livro);
        }

        /// <summary>
        /// Retorna todos os livros cadastrados.
        /// </summary>
        /// <returns>Lista de livros.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="404">Nenhum livro encontrado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarTodosLivros()
        {
            var todos = await _livroService.BuscarTodosLivrosAsync();

            if (todos == null || !todos.Any())
                return NotFound("Nenhum livro encontrado.");

            return Ok(todos);
        }

        /// <summary>
        /// Busca livros pelo título.
        /// </summary>
        /// <param name="titulo">Título do livro para pesquisa.</param>
        /// <returns>Livro(s) correspondente(s) ao título informado.</returns>
        /// <response code="200">Livro encontrado com sucesso.</response>
        /// <response code="404">Nenhum livro encontrado com o título informado.</response>
        [HttpGet("titulo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarLivroPorTitulo([FromQuery] string titulo)
        {
            var pelotitulo = await _livroService.BuscarPorTituloAsync(titulo);

            if (pelotitulo == null || !pelotitulo.Any())
                return NotFound("Nenhum livro encontrado com este título.");

            return Ok(pelotitulo);
        }

        /// <summary>
        /// Busca livros por gênero.
        /// </summary>
        /// <param name="genero">Gênero do livro (Enum EGenero).</param>
        /// <returns>Lista de livros do gênero informado.</returns>
        /// <response code="200">Livros encontrados com sucesso.</response>
        /// <response code="404">Nenhum livro encontrado para o gênero informado.</response>
        [HttpGet("genero")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarPorGenero([FromQuery] EGenero genero)
        {
            var livros = await _livroService.LivroPorGeneroAsync(genero);

            if (livros == null || !livros.Any())
                return NotFound("Nenhum livro encontrado para este gênero.");

            return Ok(livros);
        }

        /// <summary>
        /// Cadastra um novo livro no sistema.
        /// </summary>
        /// <param name="adicionarLivroRequest">Dados do livro a ser cadastrado.</param>
        /// <returns>Livro criado.</returns>
        /// <response code="201">Livro criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost("cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarLivro([FromBody] LivroRequest adicionarLivroRequest)
        {
            if (adicionarLivroRequest == null)
                return BadRequest("Os dados do livro são obrigatórios.");

            var livro = await _livroService.AdicionarLivroAsync(adicionarLivroRequest);

            if (livro == null)
                return BadRequest("Não foi possível cadastrar o livro. Verifique os dados enviados.");

            return CreatedAtAction(nameof(BuscarLivro), new { id = livro.Id }, livro);
        }

        /// <summary>
        /// Atualiza os dados de um livro existente.
        /// </summary>
        /// <param name="id">Identificador do livro.</param>
        /// <param name="atualizarLivroRequest">Novos dados do livro.</param>
        /// <returns>Livro atualizado.</returns>
        /// <response code="200">Livro atualizado com sucesso.</response>
        /// <response code="404">Livro não encontrado.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarLivro(Guid id, [FromBody] LivroRequest atualizarLivroRequest)
        {
            var atualizado = await _livroService.AtualizarLivroAsync(id, atualizarLivroRequest);

            if (atualizado == null)
                return NotFound("Livro não encontrado.");

            return Ok(atualizado);
        }

        /// <summary>
        /// Remove um livro do sistema.
        /// </summary>
        /// <param name="id">Identificador do livro.</param>
        /// <returns>Confirmação da exclusão.</returns>
        /// <response code="200">Livro removido com sucesso.</response>
        /// <response code="404">Livro não encontrado.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarLivro(Guid id)
        {
            var delete = await _livroService.DeletarLivroAsync(id);

            if (!delete)
                return NotFound("Livro não encontrado para deletar.");

            return Ok(true);
        }
    }
}
