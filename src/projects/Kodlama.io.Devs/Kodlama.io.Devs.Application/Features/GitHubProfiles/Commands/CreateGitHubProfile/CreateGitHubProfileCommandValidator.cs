using FluentValidation;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;

public class CreateGitHubProfileCommandValidator : AbstractValidator<CreateGitHubProfileCommand>
{
    public CreateGitHubProfileCommandValidator()
    {
        RuleFor(g => g.DeveloperId).NotNull();
        RuleFor(g => g.ProfileUrl).NotEmpty().NotNull();
    }
}