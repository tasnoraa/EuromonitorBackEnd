using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Utilities;
using ViewModels;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        IAccountService _accountService;
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.Register(model);

                    if (_accountService.userResults.Count == 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        foreach (var error in _accountService.userResults)
                        {
                            ModelState.AddModelError("", error);
                        }
                        return BadRequest(ModelState.Values);
                    }
                }
                return BadRequest(ModelState.Values);
            }
            catch (Exception E)
            {
                ModelState.AddModelError("", E.Message);
                return BadRequest(ModelState.Values);
            }
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {

                return Ok(await _accountService.SignIn(model));
            }
            return BadRequest(ModelState);
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

    }
}
