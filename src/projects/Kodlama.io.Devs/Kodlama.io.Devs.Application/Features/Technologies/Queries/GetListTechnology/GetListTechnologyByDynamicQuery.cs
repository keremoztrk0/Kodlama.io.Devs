using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Technologies.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Queries.GetListTechnology
{
    public class GetListTechnologyByDynamicQuery:IRequest<TechnologyListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest{ get; set; }
    }
    public class GetListTechnologyByDynamicQueryHandler : IRequestHandler<GetListTechnologyByDynamicQuery, TechnologyListModel>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;

        public GetListTechnologyByDynamicQueryHandler(IMapper mapper, ITechnologyRepository technologyRepository)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
        }

        public async Task<TechnologyListModel> Handle(GetListTechnologyByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Technology> technologies = await _technologyRepository.GetListByDynamicAsync(dynamic: request.Dynamic,
                                          include:
                                          m => m.Include(c => c.ProgrammingLanguage),
                                          index: request.PageRequest.Page,
                                          size: request.PageRequest.PageSize
                                          );
            TechnologyListModel model = _mapper.Map<TechnologyListModel>(technologies);
            return model;
        }
    }
}
