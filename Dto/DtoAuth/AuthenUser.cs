using DATN_Back_end.Dto.DtoUser;
using DATN_Back_end.Extensions;
using DATN_Back_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DATN_Back_end.Dto.DtoAuth
{
    public class AuthenUser : BaseUser
    {
        public AuthenUser(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
        }

        public AuthenUser(ClaimsIdentity claimsIdentity, User user)
        {
            Id = Guid.Parse(claimsIdentity.GetClaimValue(ClaimTypes.NameIdentifier));
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = claimsIdentity.GetClaimValue(ClaimTypes.Email);
        }

        public Claim[] GetClaims()
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Surname, LastName ?? string.Empty),
                new Claim(ClaimTypes.GivenName, FirstName ?? string.Empty),
                new Claim(ClaimTypes.Email, Email)
            };
            return claims;
        }
    }
}
