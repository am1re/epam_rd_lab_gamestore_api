using FluentValidation;

namespace Application.Publishers.Queries.GetPublishersList
{
    public class GetPublishersListQueryValidator : AbstractValidator<GetPublishersListQuery>
    {
        public GetPublishersListQueryValidator()
        {
            RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Limit).GreaterThanOrEqualTo(0);
            RuleFor(x => x.GameId).GreaterThan(0);
        }
    }
}