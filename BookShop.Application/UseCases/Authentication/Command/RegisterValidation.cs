using FluentValidation;

namespace BookShop.Application.UseCases.Authentication.Command
{
    public class RegisterValidation: AbstractValidator<RegisterUserCommand>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Username can not be null");
            RuleFor(x => x.Password).NotNull().WithMessage("Password can not be null");
        }
    }
}
