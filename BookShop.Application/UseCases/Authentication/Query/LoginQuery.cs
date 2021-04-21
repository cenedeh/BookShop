using BookShop.Application.Dto;
using BookShop.Application.Exceptions;
using BookShop.Application.Interfaces;
using BookShop.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.UseCases.Authentication.Query
{
    public class LoginQuery : IRequest<TokenDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class LoginQueryHandler : IRequestHandler<LoginQuery, TokenDto>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ITokenManager _tokenManager;
            public LoginQueryHandler(UserManager<ApplicationUser> userManager, ITokenManager tokenManager)
            {
                _userManager = userManager;
                _tokenManager = tokenManager;
            }

            public async Task<TokenDto> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Username);
                if (user == null) throw new UserAuthenticationException("invalid username or password");
                var isUserAuthenticated = await _userManager.CheckPasswordAsync(user, request.Password);
                if(!isUserAuthenticated) throw new UserAuthenticationException("invalid username or password");
                var token = _tokenManager.GenerateToken(request.Username, request.Username);
                return token;
            }
        }
    }
}
