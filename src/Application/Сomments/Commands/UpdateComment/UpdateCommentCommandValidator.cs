using FluentValidation;

namespace Application.Сomments.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Content).MinimumLength(5);
            // RuleFor(x => x.IsDeleted)
        }
    }
}