using DATN_Back_end.Dto.DtoUser;
using DATN_Back_end.Models;
using DATN_Back_end.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IRepository<User> userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<List<UserItem>> Get() => await userRepository.Get<UserItem>();

        [HttpGet("{id}")]
        public async Task<UserDetail> Get(Guid id) => await userRepository.Get<UserDetail>(id);

        [HttpPost]
        public async Task Create([FromBody] UserForm userForm) => await userRepository.Create(userForm);
    }
}
