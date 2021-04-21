using BookShop.Application.Exceptions;
using BookShop.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.UseCases.Authentication.Command
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var user = new ApplicationUser()
                {
                    UserName = request.Username,
                    Email = request.Username
                };
                var createdUser = await _userManager.CreateAsync(user, request.Password);
                if (createdUser.Succeeded) return true;
                var errors = createdUser.Errors.Select(x => x.Description).ToArray();
                var result = string.Join(";", errors);
                throw new UserAuthenticationException(result);
            }
        }
    }
}
