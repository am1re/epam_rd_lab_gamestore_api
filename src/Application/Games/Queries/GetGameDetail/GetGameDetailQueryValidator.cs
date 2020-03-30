using FluentValidation;

namespace Application.Games.Queries.GetGameDetail
{
    public class GetGameDetailQueryValidator : AbstractValidator<GetGameDetailQuery>
    {
        public GetGameDetailQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}