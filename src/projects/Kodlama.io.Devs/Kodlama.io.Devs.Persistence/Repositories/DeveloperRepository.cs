using Core.Persistence.Repositories;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using Kodlama.io.Devs.Persistence.Contexts;
using System;
using System.Linq;

namespace Kodlama.io.Devs.Persistence.Repositories
{
    public class DeveloperRepository : EfRepositoryBase<Developer, BaseDbContext>, IDeveloperRepository
    {
        public DeveloperRepository(BaseDbContext baseDbContext) : base(baseDbContext)
        {

        }
    }
}
