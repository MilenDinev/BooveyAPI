﻿namespace Boovey.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Collections.Generic;
    using AutoMapper;
    using Interfaces;
    using Data;
    using Data.Entities;
    using Models.Responses.UserModels;
    using Boovey.Models.Requests;
    using System;
    using Constants;

    public class UserService : IUserService
    {
        private readonly IUserManager userManager;
        private readonly BooveyDbContext dbContext;
        private readonly IMapper mapper;

        public UserService(IUserManager userManager, BooveyDbContext dbContext, IMapper mapper)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<RegisteredUserResponseModel> CreateAsync(RegistrationRequestModel userInput)
        {
            if (await this.userManager.FindByNameAsync(userInput.UserName) != null)
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(User), userInput.UserName));

            if (await this.userManager.FindByEmailAsync(userInput.Email) != null)
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(userInput.Email), userInput.Email));

            var user = this.mapper.Map<User>(userInput);

            await this.userManager.CreateAsync(user, userInput.Password);
            await this.userManager.AddToRoleAsync(user, "regular");

            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<RegisteredUserResponseModel>(user);
        }
        public async Task<User> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            return await this.userManager.GetUserAsync(principal);
        }

        public async Task<ICollection<UsersListingResponseModel>> GetAllUsersAsync()
        {
            var users = await this.userManager.GetAllAsync();
            var usersResponceDto = this.mapper.Map<ICollection<UsersListingResponseModel>>(users);
            return usersResponceDto.ToList();
        }
    }
}
