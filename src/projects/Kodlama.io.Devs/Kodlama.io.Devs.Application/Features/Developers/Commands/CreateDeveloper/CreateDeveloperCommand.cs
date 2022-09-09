using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Developers.Dtos;
using Kodlama.io.Devs.Application.Features.Developers.Dtos;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Developers.Commands.CreateDeveloper
{
    public class CreateDeveloperCommand:IRequest<TokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, TokenDto>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;

        public CreateDeveloperCommandHandler(IDeveloperRepository developerRepository, IMapper mapper, ITokenHelper tokenHelper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
        }

        public async Task<TokenDto> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            Developer developer = _mapper.Map<Developer>(request);
            (developer.PasswordHash, developer.PasswordSalt) = (passwordHash, passwordSalt);

            Developer createdDeveloper = await _developerRepository.AddAsync(developer);
            AccessToken token = _tokenHelper.CreateToken(createdDeveloper, new List<OperationClaim>());
            TokenDto tokenDto = _mapper.Map<TokenDto>(token);

            return tokenDto;

        }
    }
}
