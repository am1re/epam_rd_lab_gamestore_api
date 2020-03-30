using FluentValidation;

namespace Application.Games.Commands.UpdateGame
{
    public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
    {
        public UpdateGameCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Description).MinimumLength(7);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PublisherId).GreaterThan(0);
            RuleFor(x => x.DeveloperId).GreaterThan(0);
        }
    }
}