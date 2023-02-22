using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Data.Models;
using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly MealOrderingDbContext context;

        public UserService(IMapper mapper, MealOrderingDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<UserDTO> CreateUser(UserDTO user)
        {
            var dbUser = await context.Users.Where(i => i.Id == user.Id).FirstOrDefaultAsync();

            if (dbUser != null)
                throw new Exception("İlgili kayıt zaten mevcut.");

            dbUser = mapper.Map<Users>(user);

            await context.Users.AddAsync(dbUser);
            int result = await context.SaveChangesAsync();

            return mapper.Map<UserDTO>(dbUser);
        }

        public async Task<bool> DeleteUserById(Guid Id)
        {
            var dbUser = await context.Users.Where(i => i.Id == Id).FirstOrDefaultAsync();

            if (dbUser == null)
                throw new Exception("Kullanıcı bulunamadı");

            context.Users.Remove(dbUser);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            return await context.Users
                       .Where(i => i.Id == id)
                       .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync();
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return await context.Users
                         .Where(i => i.IsActive)
                         .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                         .ToListAsync();
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            var dbUser = await context.Users.Where(i => i.Id == user.Id).FirstOrDefaultAsync();

            if (dbUser == null)
                throw new Exception("İlgili kayıt bulunamadı.");
                      
            mapper.Map(user, dbUser);

            int result = await context.SaveChangesAsync();

            return mapper.Map<UserDTO>(dbUser);
        }
    }
}
