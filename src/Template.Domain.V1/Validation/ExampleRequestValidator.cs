using FluentValidation;

namespace Template.Domain.V1.Validation
{
    public class ExampleRequestValidator : AbstractValidator<Models.Request.Example>
    {
        public ExampleRequestValidator()
        {
            RuleFor(model => model.ExampleProperty).NotEmpty().WithMessage("Example validation error message");
        }
    }
}