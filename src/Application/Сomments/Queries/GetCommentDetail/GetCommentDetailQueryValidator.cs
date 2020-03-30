using FluentValidation;

namespace Application.Сomments.Queries.GetCommentDetail
{
    public class GetCommentDetailQueryValidator : AbstractValidator<GetCommentDetailQuery>
    {
        public GetCommentDetailQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Depth).GreaterThanOrEqualTo(0);
        }
    }
}