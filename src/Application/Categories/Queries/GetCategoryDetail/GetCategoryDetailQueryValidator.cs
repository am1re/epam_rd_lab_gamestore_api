using FluentValidation;

namespace Application.Categories.Queries.GetCategoryDetail
{
    public class GetCategoryDetailQueryValidator : AbstractValidator<GetCategoryDetailQuery>
    {
        public GetCategoryDetailQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}