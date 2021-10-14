using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using ViewModels;

namespace Ecommerce.Service
{
    public class AccountService: IAccountService
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;

        public AccountService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            userResults = new List<string>();
        }
        public async Task<IdentityUser> Register(RegisterViewModel model) 
        {
            var user = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var userResult = await _userManager.CreateAsync(user, model.Password);

            if (userResult.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                {
                    return user;
                }

            }

            foreach (var error in userResult.Errors)
            {
                //ModelState.AddModelError("", error.Description);
                userResults.Add(error.Description);
            }
            //return BadRequest(ModelState.Values);
            return null;

        }

        public async Task<Object> SignIn(SignInViewModel model) 
        {
            var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (signInResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var roles = await _userManager.GetRolesAsync(user);

                IdentityOptions identityOptions = new IdentityOptions();
                var claims = new Claim[]
                {
                        //new Claim("Lid","123456789"),
                        new Claim(identityOptions.ClaimsIdentity.UserIdClaimType,user.Id),
                        new Claim(identityOptions.ClaimsIdentity.UserNameClaimType,user.UserName),
                        new Claim(identityOptions.ClaimsIdentity.RoleClaimType,roles[0])
                };
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-my-secret-key"));
                var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                var jwt = new JwtSecurityToken(signingCredentials: signingCredentials,
                    claims: claims, expires: DateTime.Now.AddSeconds(60));

                return new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwt),
                    UserId = user.Id,
                    UserName = user.UserName,
                    Role = roles[0]
                };
                //return Ok(obj);
            }
            return null;
        }

        public List<string> userResults { get; set; }
    }
}
