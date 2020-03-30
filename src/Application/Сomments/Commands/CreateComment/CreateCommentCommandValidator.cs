using FluentValidation;

namespace Application.Сomments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Content).NotEmpty().MinimumLength(5);
            RuleFor(x => x.GameId).GreaterThan(0);
            RuleFor(x => x.ParentCommentId).GreaterThan(0);
        }
    }
}