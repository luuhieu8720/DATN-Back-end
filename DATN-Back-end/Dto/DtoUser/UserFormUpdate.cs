﻿using DATN_Back_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Dto.DtoUser
{
    public class UserFormUpdate
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public Guid? DepartmentId { get; set; }

        public Role Role { get; set; }
    }
}