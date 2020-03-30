using FluentValidation;

namespace Application.Developers.Commands.UpdateDeveloper
{
    public class UpdateDeveloperCommandValidator : AbstractValidator<UpdateDeveloperCommand>
    {
        public UpdateDeveloperCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        }   
    }
}