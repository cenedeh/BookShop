using BookShop.Application.Dto;
using BookShop.Application.UseCases.Authentication.Command;
using BookShop.Application.UseCases.Authentication.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This endpoint is used for login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /login
        ///     {
        ///         "Username": "chuxszee@gmail.com",
        ///         "Password": "12345"
        ///     }
        /// </remarks>
        [HttpPost("/login")]
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Login([FromBody]LoginQuery login)
        {
            var token = await _mediator.Send(login);
            return Ok(token);
        }

        /// <summary>
        /// This endpoint is used for registration
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /register
        ///     {
        ///         "Username": "chuxszee@gmail.com",
        ///         "Password": "12345"
        ///     }
        /// </remarks>
        [HttpPost("/register")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand register)
        {
            await _mediator.Send(register);
            return Ok();
        }
    }
}
