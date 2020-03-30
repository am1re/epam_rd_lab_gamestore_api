using FluentValidation;

namespace Application.Publishers.Commands.DeletePublisher
{
    public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommand>
    {
        public DeletePublisherCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}