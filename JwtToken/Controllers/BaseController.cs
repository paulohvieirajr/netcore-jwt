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
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
            
        }

        protected IActionResult OkOrNorFound(object result)
        {
            var response = new GenericResponse<object>();
            response.content = result;
            response.sucess = result != null;
            if (result == null)
                return NotFound(response);
            else
                return Ok(response);
        }
    }
}
