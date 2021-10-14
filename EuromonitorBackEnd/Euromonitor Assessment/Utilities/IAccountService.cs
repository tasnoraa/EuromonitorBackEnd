using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace Utilities
{
    public interface IAccountService
    {
        Task<IdentityUser> Register(RegisterViewModel model);
        List<string> userResults { get; set; }

        Task<Object> SignIn(SignInViewModel model);
    }
}
