using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Forms;
using Microsoft.AspNetCore.Http;
using Biblioteca.Dtos;
using Biblioteca.DAOs;
using Biblioteca.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mime;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    public class LivroController : ControllerBase
    {
        private readonly ILogger<LivroController> _logger;
        private readonly IConfiguration _config;
        private LivroDAO _livroDAO = null;

        public LivroController(ILogger<LivroController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _livroDAO = new LivroDAO(_config.GetSection("ConnectionStrings").GetSection("Default").Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> NovoLivro([FromBody] NovoLivroForm form)
        {
            var errors = new List<string>();

            try
            {
                var livro = await _livroDAO.NovoLivro(form);
                var livroDto = new LivroDto(livro);
                
                var uri = new UriBuilder();
                var path = $"{uri.Scheme}://{uri.Uri.Host}:5000";
                
                return Created($"{path}/api/livro/{livro.Id}", new GenericResponseDto("Livro criado com sucesso!", errors, livroDto));
            }
            catch (Exception e)
            {
                errors.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto("Falha ao tentar criar o livro", errors, e.Message));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(string id)
        {
            var errors = new List<string>();

            try
            {
                var livro = await _livroDAO.GetById(id);
                var livroDto = new LivroDto(livro);

                return Ok(new GenericResponseDto("Livro recuperado com sucesso!", errors, livroDto));
            }
            catch (Exception e)
            {
                errors.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto($"Falha ao tentar consultar o livro id = {id}", errors, e.Message));
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var errors = new List<string>();

            try
            {
                var livros = await _livroDAO.GetAll();
                var livrosDto = new LivrosDto(livros);

                return Ok(new GenericResponseDto($"Livros recuperados com sucesso! ({livrosDto.Livros.Count})", errors, livrosDto));
            }
            catch (Exception e)
            {
                errors.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto($"Falha ao tentar consultar todos os livros", errors, e.Message));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookForm form)
        {
            var errors = new List<string>();

            try
            {
                var livro = await _livroDAO.UpdateBook(form);
                var livroDto = new LivroDto(livro);

                return Ok(new GenericResponseDto("Livro atualizado com sucesso!", errors, livroDto));
            }
            catch (Exception e)
            {
                errors.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto($"Falha ao tentar atualizar o livro id = {form.Livro.Id.ToString()}", errors, e.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var errors = new List<string>();

            try
            {
                var deleted = await _livroDAO.DeleteBook(id);
                return Ok(new GenericResponseDto("Livro excluido com sucesso!", errors, deleted));
            }
            catch (Exception e)
            {
                errors.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto($"Falha ao tentar excluir o livro id = {id}", errors, e.Message));
            }
        }
    }
}