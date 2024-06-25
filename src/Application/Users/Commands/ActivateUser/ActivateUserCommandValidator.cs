using FluentValidation;

namespace ProjectAssignment.Application.Users.Commands
{
    public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
    {
        public ActivateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEqual(0).NotNull().WithMessage("Please provide valid user id");
        }
    }
}
