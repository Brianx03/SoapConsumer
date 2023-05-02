using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceReference1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoapConsumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("JwtToken")]
        public async Task<string> GenerateJwtToken(IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = configuration.GetValue<string>("Jwt:SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("userId", "1234") }),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = "Lalo",
                Issuer = "Brian",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet]
        [Route("Exception")]
        [ServiceFilter(typeof(AppExceptionFilter))]
        public async Task<int> SelectException() 
        {
            var number = 0;
            return 100 / number;
        }

        [HttpGet]
        [Route("ASP")]
        [Authorize]
        [ServiceFilter(typeof(AppActionFilter))]
        public async Task<ServiceReference1.User> Select([FromQuery] int userId) =>
            new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap)
                .SelectUserAsync(userId).Result.Body.SelectUserResult;

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] ServiceReference1.User user) =>
            Ok(new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap)
                .InsertUserAsync(user).Result.Body.InsertUserResult);

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ServiceReference1.User user) =>
            Ok(new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap)
                .UpdateUserAsync(user).Result.Body.UpdateUserResult);

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int userId) => 
            Ok(new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap)
                .DeleteUserAsync(userId).Result.Body.DeleteUserResult);
    }
}
