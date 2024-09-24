using FluentValidation;
using Sofisoft.Enterprise.Logging.Application.Commands;

namespace Sofisoft.Enterprise.Logging.Application.Validations
{
    public class CreateWarningValidator : AbstractValidator<CreateWarningCommand>
    {
        public CreateWarningValidator()
        {
            RuleFor(command => command.Message).MinimumLength(3).NotEmpty();
        }
    }
}