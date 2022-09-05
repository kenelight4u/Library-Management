using LM.Application.Interfaces.Utilities;
using LM.Domain.Common;
using LM.Domain.Entities;
using LM.Domain.Utils;
using LM.DTOs.Request.AuthDTO;
using LM.DTOs.Response.AuthVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LM.API.Controllers.v1
{
    /// <summary>
    /// This controller handles all account processes of this application.
    /// Registration, Update and all account verification of user.
    /// </summary>
    [ApiVersion("1.0")]
    public class AuthController : BaseController
    {
        /// <summary>
        /// The User Manager
        /// </summary>
        private readonly UserManager<LMUser> _userManager;
        /// <summary>
        /// The SignInManager
        /// </summary>
        private readonly SignInManager<LMUser> _signInManager;
        /// <summary>
        /// The RoleManagement
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;
        /// <summary>
        /// The Iconfiguration
        /// </summary>
        private readonly IConfiguration _config;
        /// <summary>
        /// The IContextAccessor
        /// </summary>
        private readonly IContextAccessor _contextAccessor;

        /// <summary>
        /// Constructor for dependency injections
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="config"></param>
        /// <param name="contextAccessor"></param>
        public AuthController(
            UserManager<LMUser> userManager,
            SignInManager<LMUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            IContextAccessor contextAccessor)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._config = config;
            this._contextAccessor = contextAccessor;
        }

        /// <summary>
        /// This endpoint registers custormers on this application.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IActionResult.</returns> 
        [HttpPost("SignUp/Customers")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO model)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists is not null)
                {
                    return BadRequest(new ResultModel<bool>
                    {
                        Data = false,
                        Message = "Email belongs to an existing customer"
                    });
                }

                var newUser = new LMUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = model.Email,
                    EmailConfirmed = true,
                    RegisteredDate = DateTime.Now,
                    PhoneNumber = model.PhoneNumber,
                    IsActive = true
                };

                var isCreated = await _userManager.CreateAsync(newUser, model.Password);
                if (isCreated.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(CoreConstants.Customer).Result)
                    {
                        var role = new IdentityRole()
                        {
                            Name = CoreConstants.Customer
                        };
                        IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

                        if (roleResult.Succeeded)
                            _userManager.AddToRoleAsync(newUser, CoreConstants.Customer).Wait();
                    }
                    else
                    {
                        _userManager.AddToRoleAsync(newUser, CoreConstants.Customer).Wait();
                    }

                    return Ok(new ResultModel<bool>
                    {
                        Data = true,
                        Message = "Registered successfully!",
                    });
                }
            }
            return BadRequest(new ResultModel<bool>
            {
                Data = false,
                Message = "All fields are required!",
            });
        }

        /// <summary>
        /// This endpoint registers Clients on this application.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IActionResult.</returns> 
        [HttpPost("SignUp/Clients")]
        [AllowAnonymous]
        public async Task<IActionResult> ClientsSignUp([FromBody] SignUpDTO model)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists is not null)
                {
                    return BadRequest(new ResultModel<bool>
                    {
                        Data = false,
                        Message = "Email belongs to an existing Client",
                    });
                }

                var newUser = new LMUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = model.Email,
                    EmailConfirmed = true,
                    RegisteredDate = DateTime.Now,
                    PhoneNumber = model.PhoneNumber,
                    IsActive = true
                };

                var isCreated = await _userManager.CreateAsync(newUser, model.Password);
                if (isCreated.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(CoreConstants.Client).Result)
                    {
                        var role = new IdentityRole()
                        {
                            Name = CoreConstants.Client
                        };
                        IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

                        if(roleResult.Succeeded)
                            _userManager.AddToRoleAsync(newUser, CoreConstants.Client).Wait();
                    }
                    else
                    {
                        _userManager.AddToRoleAsync(newUser, CoreConstants.Client).Wait();
                    }

                    return Ok(new ResultModel<bool>
                    {
                        Data = true,
                        Message = "Registered successfully!",
                    });
                }
            }
            return BadRequest(new ResultModel<bool>
            {
                Data = false,
                Message = "All fields are required!",
            });
        }

        /// <summary>
        /// This endpoint signs in a user whose correct credentials are sent in.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO model)
        {
            if (ModelState.IsValid)
            {
                // check if the user with the same email exist
                var user = await _userManager.FindByEmailAsync(model.Email.Trim());

                if (user == null)
                {
                    // We dont want to give to much information on why the request has failed for security reasons
                    return BadRequest(new ResultModel<bool>()
                    {
                        Data = false,
                        Message = "User with this email doesn't exixts"
                    });
                }

                // Now we need to check if the user has inputed the right password
                var isCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

                if (isCorrect)
                {
                    if (user.LockoutEnabled)
                    {
                        return Ok(new ResultModel<bool>() { Message = "Your account has been suspended! Contact Admin.", Data = false });
                    }
                    var jwtToken = await GenerateJWT(user);

                    return Ok(new ResultModel<SignInResponsDTO>()
                    {
                        Message = "Signed Successfully!",
                        Data = new SignInResponsDTO
                        {
                            Email = user.Email.Trim(),
                            Fullname = $"{user.FirstName} {user.LastName}",
                            UserId = Guid.Parse(user.Id),
                            Token = jwtToken,
                            Role = _userManager.GetRolesAsync(user).Result.ToList()
                        }
                    });
                }
                else
                {
                    // We dont want to give too much information on why the request has failed for security reasons
                    return BadRequest(new ResultModel<bool>()
                    {
                        Data = false,
                        Message = "Invalid authentication request"
                    });
                }
            }
            return BadRequest(new ResultModel<bool>()
            {
                Data = false,
                Message = "Invalid signin request"
            });
        }

        /// <summary>
        /// This endpoint Updates Users Details on this application.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IActionResult.</returns> 
        [HttpPut("UpdateAccount")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateAccount([FromForm] EditDTO model)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) 
                return BadRequest(new ResultModel<bool> 
                { 
                    Data = false, 
                    Message = "User doesn't Exist" 
                });

            user.FirstName = model.FirstName is null ? user.FirstName : model.FirstName;
            user.LastName = model.LastName is null ? user.LastName : model.LastName;
            user.PhoneNumber = model.PhoneNumber is null ? user.PhoneNumber : model.PhoneNumber;

            await _userManager.UpdateAsync(user);
            return Ok(new ResultModel<bool>
            {
                Data = true,
                Message = "Updated Successfully!"
            });

        }

        /// <summary>
        /// This endpoint changes user's account password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IActionResult.</returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.NewPassword) || string.IsNullOrWhiteSpace(model.OldPassword))
            {
                return BadRequest(new ResultModel<bool> { Message = "All fields are required!", Data = false });
            }
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user is null)
                {
                    return BadRequest(new ResultModel<bool> { Message = "User Not Found!", Data = false });
                }

                var result = await _userManager.ChangePasswordAsync(user,
                    model.OldPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    return BadRequest(new ResultModel<bool> { Message = result.Errors.FirstOrDefault().Description, Data = false });
                }
                await _signInManager.RefreshSignInAsync(user);
                return Ok(new ResultModel<bool> { Message = "Password changed successfully!", Data = true });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint logs out users.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// This method generates access token for the user that is passed into it.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string Token</returns>
        private async Task<string> GenerateJWT(LMUser user)
        {

            //get the user's roles
            var roles = await _userManager.GetRolesAsync(user);
            //Generate Token
            var expirationDay = DateTime.UtcNow.AddDays(2);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JwtSettings:Secret"].ToString()));
            List<Claim> subjectClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            subjectClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            subjectClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            subjectClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(subjectClaims.AsEnumerable()),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Expires = expirationDay

            };
            //create the token 
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
