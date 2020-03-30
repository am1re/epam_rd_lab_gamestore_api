using FluentValidation;

namespace Application.Publishers.Queries.GetPublisherDetail
{
    public class GetPublisherDetailQueryValidator : AbstractValidator<GetPublisherDetailQuery>
    {
        public GetPublisherDetailQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}