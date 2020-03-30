using FluentValidation;

namespace Application.Publishers.Commands.CreatePublisher
{
    public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
    {
        public CreatePublisherCommandValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3);
        }
    }
}