using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtToken.Dto;
using JwtToken.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JwtToken.Controllers
{
    [ApiController]
    public class TokenController : BaseController
    {
        private IConfiguration _configuration { get; }

        public TokenController([FromServices] IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [ExceptionWebApiFilter]
        [LogActionWebApiFilter]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(GenericResponse<Token>))]
        [ProducesResponseType(400, Type = typeof(GenericResponse<string>))]
        [ProducesResponseType(500, Type = typeof(GenericResponse<string>))]
        public IActionResult Token([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GenericResponse<string>() { sucess = false, error_message = "Credenciais inválidas..." });
            }

            var isValid = CustomAuthenticate(request.Username, request.Password);
            if (!isValid)
            {
                return BadRequest(new GenericResponse<string>() { sucess = false, error_message = "Autenticação inválida." });
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SimetricKey"])), SecurityAlgorithms.HmacSha256)
            );

            return OkOrNorFound(new GenericResponse<Token>()
            {
                sucess = true,
                content = new Token()
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    token_type = "bearer",
                    created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    expires_in = (long)TimeSpan.FromMinutes(DateTime.UtcNow.AddDays(1).Minute).TotalSeconds,
                    message = "OK"
                }
            });
        }

        private bool CustomAuthenticate(string username, string password)
        {
            return username == _configuration["AppSettings:username"]
                && password == _configuration["AppSettings:password"];
        }
    }
}
