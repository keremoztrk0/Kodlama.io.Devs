using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Developers.Rules
{
    public class DeveloperBusinessRules
    {
        public void UserShouldExist(User user)
        {
            if (user == null) throw new BusinessException("User does not exist");
        }

        public void UserCredentialsShouldMatch(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (!result) throw new BusinessException("Credentials are wrong or empty");
        }
    }
}
