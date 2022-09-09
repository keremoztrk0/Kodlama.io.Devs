using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommandValidator : AbstractValidator<UpdateGitHubProfileCommand>
    {
        public UpdateGitHubProfileCommandValidator()
        {
            RuleFor(g => g.Id).NotNull();
            RuleFor(g => g.ProfileUrl).NotNull().NotEmpty();
        }
    }
}
