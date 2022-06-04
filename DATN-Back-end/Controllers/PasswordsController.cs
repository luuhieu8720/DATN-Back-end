using DATN_Back_end.Dto.DtoPassword;
using DATN_Back_end.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Controllers
{
    [Route("api/password")]
    public class PasswordsController : ControllerBase
    {
        private readonly IPasswordService passwordService;

        public PasswordsController(IPasswordService passwordService)
        {
            this.passwordService = passwordService;
        }

        [HttpPost("forget")]
        public async Task Forget(string email) => await passwordService.Forget(email);

        [HttpPost("change")]
        public async Task Change(ChangePasswordForm changePasswordForm) => await passwordService.Change(changePasswordForm);
    }
}
