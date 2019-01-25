using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NasGrad.API.Auth;
using NasGrad.API.DTO;
using NasGrad.DBEngine;

namespace NasGrad.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IDBStorage _dbStorage;
        private AppSettings _appSettings;

        public LoginController(IDBStorage dbStorage, IOptions<AppSettings> appSettings)
        {
            _dbStorage = dbStorage;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO data)
        {
            var user = await _dbStorage.GetUser(data.Username);
            if (user == null)
                return BadRequest("Username or password is incorrect");

            var isValidPassword = AuthUtil.ValidatePassword(user.Salt, user.PasswordHash, data.Password);
            if (!isValidPassword)
                return BadRequest("Username or password is incorrect");

            var role = await _dbStorage.GetRole(user.RoleId);
            var ci = AuthUtil.GenerateClaimsIdentity(user.Username, user.Id, role);

            var options = new AuthUtil.JwtOptions(_appSettings.JwtTokenValidity)
            {
                Issuer = _appSettings.JwtIssuer,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.JwtSecret)),
                        SecurityAlgorithms.HmacSha256)
            };

            return Ok(new
            {
                jwt_token = AuthUtil.EncodeJWTToken(ci, options),
                expiration_after = options.ValidFor.TotalSeconds,
            });
        }
    }
}