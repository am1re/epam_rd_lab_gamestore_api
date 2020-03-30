using FluentValidation;

namespace Application.Developers.Queries.GetDevelopersList
{
    public class GetDevelopersListQueryValidator : AbstractValidator<GetDevelopersListQuery>
    {
        public GetDevelopersListQueryValidator()
        {
            RuleFor(x => x.GameId).GreaterThan(0);
            RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Limit).GreaterThanOrEqualTo(0);
        }
    }
}