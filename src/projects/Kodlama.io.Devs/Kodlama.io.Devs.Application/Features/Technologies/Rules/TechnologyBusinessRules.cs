using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kodlama.io.Devs.Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository TechnologyRepository)
        {
            _technologyRepository = TechnologyRepository;
        }
        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name already exist.");
        }

        public async Task TechnologyCannotBeDuplicatedWhileUpdating(int id, string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(b => b.Name == name && b.Id != id);
            if (result.Items.Any()) throw new BusinessException("Technology name already exist.");
        }

        public void TechnologyShouldExistWhenRequested(Technology Technology)
        {
            if (Technology == null) throw new BusinessException("Requested technology does not exist");
        }
        public async Task TechnologyShouldExistWhenRequested(int id)
        {
            Technology result = await _technologyRepository.GetAsync(b => b.Id == id);

            if(result==null)throw new BusinessException("Requested technology does not exist");
        }
    }
}
