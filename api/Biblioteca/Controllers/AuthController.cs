using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using Biblioteca.DAOs;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Biblioteca.Forms;
using Biblioteca.Dtos;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;
        private AuthDAO _authDAO = null;

        public AuthController(ILogger<AuthController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _authDAO = new AuthDAO(_config.GetSection("ConnectionStrings").GetSection("Default").Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginForm form)
        {
            var errors = new List<string>();

            if(!ModelState.IsValid)
                return BadRequest(new GenericResponseDto("Verifique os dados e tente novamente!", errors, ModelState));

            try
            {
                if (await _authDAO.ExistsAccount(form.Username))
                {
                    var account = await _authDAO.FindAccount(form.Username);
                    var accountDto = new AccountDto(account);
                    
                    if (account.Password.Equals(form.Password))                    
                        return Ok(new GenericResponseDto("O login foi um sucesso!", errors, accountDto));
                }
                
                errors.Add("Favor verificar os dados.");
                return BadRequest(new GenericResponseDto("Usuário ou senha inválido.", errors, form));
            }
            catch (Exception e)
            {
                errors.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto("Falha ao tentar logar.", errors, e.Message));
            }
        }

        [HttpPost("create-account")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountForm form)
        {
            var errors = new List<string>();

            if(!ModelState.IsValid)
                return BadRequest(new GenericResponseDto("Verifique os dados e tente novamente!", errors, ModelState));

            try
            {
                var existsAccount = await _authDAO.ExistsAccount(form.Username);

                if (!existsAccount)
                {
                    if (form.Password.Equals(form.ConfirmPassword))
                    {
                        var account = await _authDAO.CreateAccount(form);
                        var accountDto = new AccountDto(account);
                        var uri = new UriBuilder();
                        var path = $"{uri.Scheme}://{uri.Uri.Host}:5000";
                        return Created($"{path}/api/account/{accountDto.Username}", new GenericResponseDto("Conta criada com sucesso!", errors, accountDto));
                    }
                    
                    errors.Add("Favor verificar os dados.");
                    return BadRequest(new GenericResponseDto("Senhas não conferem.", errors, form));
                    
                }
                
                errors.Add("Favor verificar os dados.");
                return BadRequest(new GenericResponseDto($"Nome de usuário {form.Username} já existe.", errors, form));

            }
            catch (Exception e)
            {
                errors.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto("Falha ao tentar criar nova conta", errors, e.Message));
            }
        }

    }
}