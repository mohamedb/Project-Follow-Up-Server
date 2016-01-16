﻿using System.Security.Principal;
using PUp.Models.Entity;
using PUp.Models.Repository;

namespace PUp.Models.Repository
{
   public  interface IUserRepository:IRepository<UserEntity>
    {
        UserEntity GetCurrentUser();
        UserEntity FindById(string id);
        void AddContribution(ContributionEntity c);
        UserEntity GetFirstOrDefault();
        UserEntity FindByEmail(string email);
    }
}