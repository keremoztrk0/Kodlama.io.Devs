using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Developers.Commands.CreateDeveloper
{
    public class CreateDeveloperCommandValidator:AbstractValidator<CreateDeveloperCommand>
    {
        public CreateDeveloperCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.FirstName).MinimumLength(2).NotEmpty().NotNull();
            RuleFor(x => x.LastName).MinimumLength(2).NotEmpty().NotNull();
            RuleFor(x => x.Password).MinimumLength(6).NotEmpty().NotNull();

        }
    }
}
