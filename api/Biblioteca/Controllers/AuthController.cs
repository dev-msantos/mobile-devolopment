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
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private AuthDAO AuthDAO = null;

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            AuthDAO = new AuthDAO(_configuration.GetSection("ConnectionStrings").GetSection("Default").Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginForm form)
        {
            List<string> errorList = new List<string>();

            try
            {
                bool existsAccount = await AuthDAO.ExistsAccount(form.Username);

                if (existsAccount)
                {
                    Account account = await AuthDAO.FindAccount(form.Username);
                    AccountDto accountDto = new AccountDto(account);

                    if (account.Password.Equals(form.Password))
                    {
                        return Ok(new GenericResponseDto("O login foi um sucesso!", errorList, accountDto));
                    }
                }
                
                errorList.Add("Favor verificar os dados.");
                return BadRequest(new GenericResponseDto("Usuário ou senha inválido.", errorList, form));
            }
            catch (Exception ex)
            {
                errorList.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto("Falha ao tentar logar.", errorList, ex));
            }
        }

        [HttpPost("create-account")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountForm form)
        {
            List<string> errorList = new List<string>();

            try
            {
                bool existsAccount = await AuthDAO.ExistsAccount(form.Username);

                if (!existsAccount)
                {
                    if (form.Password.Equals(form.Password2))
                    {
                        Account account = await AuthDAO.CreateAccount(form);
                        AccountDto accountDto = new AccountDto(account);
                        var uri = new UriBuilder();
                        var path = $"{uri.Scheme}://{uri.Uri.Host}:5000";
                        return Created($"{path}/api/account/{accountDto.Username}", new GenericResponseDto("Conta criada com sucesso!", errorList, accountDto));
                    }
                    else
                    {
                        errorList.Add("Favor verificar os dados.");
                        return BadRequest(new GenericResponseDto("Senhas não conferem.", errorList, form));
                    }
                } else
                {
                    errorList.Add("Favor verificar os dados.");
                    return BadRequest(new GenericResponseDto($"Nome de usuário {form.Username} já existe.", errorList, form));
                }

            }
            catch (Exception ex)
            {
                errorList.Add("Verifique os dados enviados");
                return BadRequest(new GenericResponseDto("Falha ao tentar criar nova conta", errorList, ex));
            }
        }

    }
}