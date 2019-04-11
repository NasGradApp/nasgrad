using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NasGrad.API.Auth;
using NasGrad.DBEngine;
using System.Threading.Tasks;

namespace NasGrad.API.Controllers
{
    [Route("security/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IDBStorage _dbStorage;
        private AppSettings _appSettings;

        public AuthController(IDBStorage dbStorage, IOptions<AppSettings> appSettings)
        {
            _dbStorage = dbStorage;
            _appSettings = appSettings.Value;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return new ContentResult()
            {
                ContentType = "text/html",
                Content = "<div>" +
                          "<h2>Unauthorized, please login to your account</h2>" +
                          "<form action=\"LoginPost\" method=\"post\">" +
                          "<input type=\"text\" name=\"username\" placeholder=\"username\" />" +
                          "<input type=\"password\" name = \"password\" placeholder = \"password\" />" +
                          "<button type=\"submit\" class=\"btn\">Login</button>" +
                          "</form>" +
                          "</div>",
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        [HttpPost("LoginPost")]
        public async Task<IActionResult> LoginPost([FromForm] string username, [FromForm] string password)
        {
            var user = await _dbStorage.GetUser(username);
            if (user == null)
                return BadRequest("Username or password is incorrect");

            var isValidPassword = AuthUtil.ValidatePassword(user.Salt, user.PasswordHash, password);
            if (!isValidPassword)
                return BadRequest("Username or password is incorrect");

            var role = await _dbStorage.GetRole(user.RoleId);

            if(role.Type == (int)AuthRoleType.ReadOnly)
            {
                return Redirect("/swagger/");
            }

            return BadRequest("Username or password is incorrect"); ;
        }
    }
}