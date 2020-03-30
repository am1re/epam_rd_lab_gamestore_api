using FluentValidation;

namespace Application.Games.Commands.CreateGame
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Description).MinimumLength(7);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PublisherId).GreaterThan(0);
            RuleFor(x => x.DeveloperId).GreaterThan(0);
        }
    }
}