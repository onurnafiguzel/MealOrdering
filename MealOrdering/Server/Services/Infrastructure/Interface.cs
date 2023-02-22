using MealOrdering.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Infrastructure
{
    public interface IUserService
    {
        public Task<UserDTO> GetUserById(Guid guid);
        public Task<List<UserDTO>> GetUsers();
        public Task<UserDTO> CreateUser(UserDTO user);
        public Task<UserDTO> UpdateUser(UserDTO user);
        public Task<bool> DeleteUserById(Guid Id);
    }
}
