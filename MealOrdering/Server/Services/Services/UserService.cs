using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Services
{
    public class UserService : IUserService
    {
        public Task<UserDTO> CreateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUserById(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDTO>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> UpdateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
