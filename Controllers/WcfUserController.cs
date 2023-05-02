using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReference2;

namespace SoapConsumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WcfUserController : Controller
    {
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(AppActionFilter))]
        public async Task<User> SelectWcf([FromQuery] int userId) =>
            new ServiceReference2.ServiceClient().SelectUserAsync(userId).Result;
    }
}
