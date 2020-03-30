using FluentValidation;

namespace Application.Сomments.Queries.GetCommentsList
{
    public class GetCommentsListQueryValidator : AbstractValidator<GetCommentsListQuery>
    {
        public GetCommentsListQueryValidator()
        {
            RuleFor(x => x.Depth).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Limit).GreaterThanOrEqualTo(0);
            RuleFor(x => x.GameId).GreaterThan(0);
        }
    }
}