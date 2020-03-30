using FluentValidation;

namespace Application.Games.Commands.DeleteGame
{
    public class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
    {
        public DeleteGameCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}