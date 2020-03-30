using FluentValidation;

namespace Application.Developers.Commands.DeleteDeveloper
{
    public class DeleteDeveloperCommandValidator : AbstractValidator<DeleteDeveloperCommand>
    {
        public DeleteDeveloperCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}