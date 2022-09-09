using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Linq;

namespace Kodlama.io.Devs.Application.Services.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
    {
    }
}
