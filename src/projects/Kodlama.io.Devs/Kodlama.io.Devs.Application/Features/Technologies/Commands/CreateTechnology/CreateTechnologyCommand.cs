using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Features.Technologies.Dtos;
using Kodlama.io.Devs.Application.Features.Technologies.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
    {
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }
    }
    public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyBusinessRules _technologyBusinessRules;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules, TechnologyBusinessRules technologyBusinessRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);
            await _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);

            Technology mappedTechnology = _mapper.Map<Technology>(request);
            Technology createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
            CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);
            
            return createdTechnologyDto;
        }
    }
}
