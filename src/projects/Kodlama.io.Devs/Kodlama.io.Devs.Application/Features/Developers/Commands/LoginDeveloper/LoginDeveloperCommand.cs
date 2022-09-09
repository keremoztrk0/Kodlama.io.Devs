using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Developers.Dtos;
using Kodlama.io.Devs.Application.Features.Developers.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;

namespace Kodlama.io.Devs.Application.Features.Developers.Commands.LoginDeveloper
{
    public class LoginDeveloperCommand : IRequest<TokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginDeveloperCommandHandler : IRequestHandler<LoginDeveloperCommand, TokenDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        private readonly DeveloperBusinessRules _developerBusinessRules;

        public LoginDeveloperCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, DeveloperBusinessRules developerBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _developerBusinessRules = developerBusinessRules;
        }

        public async Task<TokenDto> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetAsync(
                u => u.Email.ToLower() == request.Email.ToLower(),
                include: m => m.Include(c => c.UserOperationClaims).ThenInclude(x => x.OperationClaim));
            _developerBusinessRules.UserShouldExist(user);

            List<OperationClaim> operationClaims = new() { };
            foreach (var userOperationClaim in user.UserOperationClaims)
            {
                operationClaims.Add(userOperationClaim.OperationClaim);
            }

            _developerBusinessRules.UserCredentialsShouldMatch(request.Password, user.PasswordHash, user.PasswordSalt);

            AccessToken token = _tokenHelper.CreateToken(user, operationClaims);

            TokenDto tokenDto = _mapper.Map<TokenDto>(token);

            return tokenDto;
        }
    }
}
