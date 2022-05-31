using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using DATN_Back_end.Models;
using DATN_Back_end.Dto.DtoUser;
using DATN_Back_end.Extensions;

namespace DATN_Back_end.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task Create(UserForm userForm)
        {
            userForm.Password = userForm.Password.Encrypt();

            await base.Create(userForm);
        }

        public async Task<User> GetById(Guid Id)
        {
            return await dataContext.Users.FindAsync(Id);
        }
    }
}
