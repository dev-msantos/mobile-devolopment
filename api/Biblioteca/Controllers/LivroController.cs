using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Forms;
using Microsoft.AspNetCore.Http;
using Biblioteca.Dtos;
using Biblioteca.DAOs;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILogger<LivroController> _logger;
        private LivroDAO LivroDAO = new LivroDAO();

        public LivroController(ILogger<LivroController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> NovoLivro([FromBody] NovoLivroForm form)
        {
            List<string> errorList = new List<string>();

            try
            {
                Livro livro = await LivroDAO.NovoLivro(form);
                LivroDto livroDto = new LivroDto(livro);

                return Created("", new GenericResponseDto("Livro criado com sucesso!", errorList, livroDto));
            }
            catch (Exception)
            {
                errorList.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto("Falha ao tentar criar o livro", errorList, form));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            List<string> errorList = new List<string>();

            try
            {
                Livro livro = await LivroDAO.GetById(id);
                LivroDto livroDto = new LivroDto(livro);

                return Ok(new GenericResponseDto("Livro recuperado com sucesso!", errorList, livroDto));
            }
            catch (Exception)
            {
                errorList.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto($"Falha ao tentar consultar o livro id = {id}", errorList, id));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            List<string> errorList = new List<string>();

            try
            {
                List<Livro> livros = await LivroDAO.GetAll();
                LivrosDto livrosDto = new LivrosDto(livros);

                return Ok(new GenericResponseDto($"Livros recuperados com sucesso! ({livrosDto.Livros.Count})", errorList, livrosDto));
            }
            catch (Exception)
            {
                errorList.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto($"Falha ao tentar consultar todos os livros", errorList, new List<string>()));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookForm form)
        {
            List<string> errorList = new List<string>();

            try
            {
                Livro livro = await LivroDAO.UpdateBook(form);
                LivroDto livroDto = new LivroDto(livro);

                return Ok(new GenericResponseDto("Livro atualizado com sucesso!", errorList, livroDto));
            }
            catch (Exception)
            {
                errorList.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto($"Falha ao tentar atualizar o livro id = {form.Livro.Id}", errorList, form));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBook([FromQuery] string id)
        {
            List<string> errorList = new List<string>();

            try
            {
                bool deleted = await LivroDAO.DeleteBook(id);
                return Ok(new GenericResponseDto("Livro excluido com sucesso!", errorList, deleted));
            }
            catch (Exception)
            {
                errorList.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto($"Falha ao tentar excluir o livro id = {id}", errorList, id));
            }
        }
    }
}