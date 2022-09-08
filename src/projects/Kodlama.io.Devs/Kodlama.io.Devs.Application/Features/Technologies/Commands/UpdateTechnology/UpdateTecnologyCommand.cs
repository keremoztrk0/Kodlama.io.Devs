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

namespace Kodlama.io.Devs.Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }
    }

    public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly TechnologyBusinessRules _technologyBusinessRules;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public UpdateTechnologyCommandHandler(
            IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules,
            ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
            _technologyBusinessRules = technologyBusinessRules;
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
        }

        public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _technologyBusinessRules.TechnologyCannotBeDuplicatedWhileUpdating(request.Id, request.Name);
            await _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);

            Technology technology = _mapper.Map<Technology>(request);
            Technology updatedTechnology = await _technologyRepository.UpdateAsync(technology);
            UpdatedTechnologyDto updatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

            return updatedTechnologyDto;
        }
    }
}
