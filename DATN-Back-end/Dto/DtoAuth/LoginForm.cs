using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Dto.DtoAuth
{
    public class LoginForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username cannot be null")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be null")]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
