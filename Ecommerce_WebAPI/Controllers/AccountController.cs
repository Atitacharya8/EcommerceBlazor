using Ecommerce_Common;
using Ecommerce_DataAccess;
using Ecommerce_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDTO)
        {
            if(signUpRequestDTO == null || !ModelState.IsValid) 
            {
                return BadRequest();
            }

            //We should create user, result with user and password, and assign the user a role which is Role_Customer by default
            var User = new ApplicationUser
            {
                UserName = signUpRequestDTO.Email,
                Email = signUpRequestDTO.Email,
                Name = signUpRequestDTO.Name,
                PhoneNumber = signUpRequestDTO.PhoneNumber,
                EmailConfirmed=true

            };

            var result = await _userManager.CreateAsync(User, signUpRequestDTO.Password);

            if(!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegisterationSuccessful = false,
                    Errors = result.Errors.Select(u=>u.Description)
                }); ;
            }

            var roleResult = await _userManager.AddToRoleAsync(User, SD.Role_Customer);

            if (!roleResult.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegisterationSuccessful = false,
                    Errors = result.Errors.Select(u => u.Description)
                }); ;
            }

            return StatusCode(201); //201 means record has been created and request has been fulfilled

        }
    }
}
