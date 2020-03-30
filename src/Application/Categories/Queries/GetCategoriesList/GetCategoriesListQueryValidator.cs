using FluentValidation;

namespace Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryValidator : AbstractValidator<GetCategoriesListQuery>
    {
        public GetCategoriesListQueryValidator()
        {
            RuleFor(x => x.GameId).GreaterThan(0);
            RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Limit).GreaterThanOrEqualTo(0);
        }
    }
}