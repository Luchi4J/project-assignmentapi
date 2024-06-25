using FluentValidation;

namespace ProjectAssignment.Application.Users.Commands
{
    public class DeactivateUserCommandValidator : AbstractValidator<DeactivateUserCommand>
    {
        public DeactivateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEqual(0).NotNull().WithMessage("Please provide valid user id");
        }
    }
}
