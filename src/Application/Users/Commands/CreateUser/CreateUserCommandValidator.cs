using FluentValidation;

namespace ProjectAssignment.Application.User.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("Please provide first name");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Please provide last name");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Please provide valid email");
        }
    }
}
