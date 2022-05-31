﻿using DATN_Back_end.Dto.DtoUser;
using DATN_Back_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid Id);

        Task Create(UserForm userForm);
    }
}
