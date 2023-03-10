using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Data.Models;
using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly MealOrderingDbContext context;
        private readonly IConfiguration configuration;

        public UserService(IMapper mapper, MealOrderingDbContext context, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.context = context;
            this.configuration = configuration;
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

        public string Login(string Email, string Password)
        {
            // Database operations about veriy user

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(int.Parse(configuration["JwtExpiryInDays"].ToString()));

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,Email)
            };

            var token = new JwtSecurityToken(configuration["JwtIssuer"], configuration["JwtAudience"], claims, null, expiry, creds);

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;
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
