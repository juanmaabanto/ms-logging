using FluentValidation;
using Sofisoft.Enterprise.Logging.Application.Commands;

namespace Sofisoft.Enterprise.Logging.Application.Validations
{
    public class CreateErrorValidator : AbstractValidator<CreateErrorCommand>
    {
        public CreateErrorValidator()
        {
            RuleFor(command => command.Message).MinimumLength(3).NotEmpty();
        }
    }
}