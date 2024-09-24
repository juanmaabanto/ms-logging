using FluentValidation;
using Sofisoft.Enterprise.Logging.Application.Commands;

namespace Sofisoft.Enterprise.Logging.Application.Validations
{
    public class CreateInformationValidator : AbstractValidator<CreateInformationCommand>
    {
        public CreateInformationValidator()
        {
            RuleFor(command => command.Message).MinimumLength(3).NotEmpty();
        }
    }
}