using FluentValidation;

namespace Application.Developers.Queries.GetDeveloperDetail
{
    public class GetDeveloperDetailQueryValidator : AbstractValidator<GetDeveloperDetailQuery>
    {
        public GetDeveloperDetailQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}