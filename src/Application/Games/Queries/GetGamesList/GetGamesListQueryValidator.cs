using FluentValidation;

namespace Application.Games.Queries.GetGamesList
{
    public class GetGamesListQueryValidator : AbstractValidator<GetGamesListQuery>
    {
        public GetGamesListQueryValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.DeveloperId).GreaterThan(0);
            RuleFor(x => x.PublisherId).GreaterThan(0);
            RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Limit).GreaterThanOrEqualTo(0);
        }
    }
}