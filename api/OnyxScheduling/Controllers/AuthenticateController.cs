using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnyxScheduling.Auth;
using OnyxScheduling.Dtos;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnyxScheduling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;

        public AuthenticateController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IAccountRepository accountRepository
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _accountRepository = accountRepository;
        }

        private async Task<bool> CheckCompanyCode(string incomingCode, string codeFound)
        {
            if (incomingCode == codeFound)
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return NoContent();
            }

        
            if (!await CheckCompanyCode(model.CompanyId.ToUpper(), user.CompanyId.ToUpper()))
            {
                return BadRequest(error:"Company Id does not match");
            }
            var validUserRole = await _userManager.GetRolesAsync(user);
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (user.UserName != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("CompanyId", user.CompanyId) 
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);

                    UserDto returnUser = new UserDto()
                    {
                        UserName = user.UserName,
                        Role = validUserRole[0]
                    };
                    return Ok(new 
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        role = validUserRole[0],
                        userName = user.UserName,
                        userId = user.Id,
                        companyId = user.CompanyId
                    });
                }
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            // Usernames cannot be shared among the same company
            if (userExists != null && userExists.CompanyId == model.CompanyId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error", Message = "User already exists!"
                });
            }

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName, 
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                Role = model.Role,
                Phone = model.Phone,
                State = model.State,
                CompanyId = model.CompanyId
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error", Message = "User creation failed! Please check user details and try again."
                });

            if (!await _roleManager.RoleExistsAsync(Auth.UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(Auth.UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(Auth.UserRoles.Office))
                await _roleManager.CreateAsync(new IdentityRole(Auth.UserRoles.Office));

            if (!await _roleManager.RoleExistsAsync(Auth.UserRoles.Field))
                await _roleManager.CreateAsync(new IdentityRole(Auth.UserRoles.Field));
            
            if (!await _roleManager.RoleExistsAsync(Auth.UserRoles.Customer))
                await _roleManager.CreateAsync(new IdentityRole(Auth.UserRoles.Customer));

            switch (model.Role)
            {
                case Auth.UserRoles.Office:
                    await _userManager.AddToRoleAsync(user, Auth.UserRoles.Office);
                    break;
                    
                case Auth.UserRoles.Field:
                    await _userManager.AddToRoleAsync(user, Auth.UserRoles.Field);
                    break;

                case Auth.UserRoles.Admin:
                    await _userManager.AddToRoleAsync(user, Auth.UserRoles.Admin);
                    break;

                case Auth.UserRoles.Customer:
                    await _userManager.AddToRoleAsync(user, Auth.UserRoles.Customer);
                    break;
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpGet]
        [Route("GetUserFromId")]
        public async Task<ActionResult<UserDto>> LoadUser(string userId)
        {
            var resultUser = await _accountRepository.GetTechnciainsFromTechId(userId);

            var userDto = new UserDto()
            {
                Id = resultUser.Id, 
                UserName = resultUser.UserName,
                FirstName = resultUser.FirstName,
                LastName = resultUser.LastName,
                City = resultUser.City,
                Address = resultUser.Address,
                State = resultUser.State,
                Phone = resultUser.Phone,
                Email = resultUser.Email,
                Role = resultUser.Role,
                CompanyId = resultUser.CompanyId
            };

            return Ok(userDto);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512)
                );

            return token;
        }

        [HttpPut]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _accountRepository.GetTechnciainsFromTechId(resetPasswordDto.UserId);
            if (user.CompanyId != resetPasswordDto.CompanyId)
            {
                return NotFound("Company ID does not match");
            }
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            
            if (!await _userManager.CheckPasswordAsync(user, resetPasswordDto.CurrentPassword))
            {
                return BadRequest("Password does not match current");

            }
            await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordDto.NewPassword);
            return NoContent();
        }

        [HttpPut]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {            
            var user = await _accountRepository.GetTechnciainsFromTechId(resetPasswordDto.UserId);
            if (user.CompanyId != resetPasswordDto.CompanyId)
            {
                return NotFound("Company ID does not match");
            }
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordDto.NewPassword);
            return NoContent();
        }
    }
}
   
