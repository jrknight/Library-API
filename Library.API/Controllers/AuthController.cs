using Entities;
using Library.API.Filters;
using Library.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private ReCircleDbContext ctx;
        private SignInManager<User> signInMgr;
        private UserManager<User> userMgr;
        private IPasswordHasher<User> passwordHasher;
        private IConfigurationRoot config;

        public AuthController(ReCircleDbContext context,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IPasswordHasher<User> hasher,
            IConfigurationRoot config)
        {
            ctx = context;
            signInMgr = signInManager;
            userMgr = userManager;
            passwordHasher = hasher;
            this.config = config;
        }

        [ValidateModel]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {
            try
            {
                var result = await signInMgr.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return BadRequest("Failed to log in.");
        }


        [ValidateModel]
        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModel model)
        {
            try
            {
                var user = await userMgr.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if ((await signInMgr.CheckPasswordSignInAsync(user, model.Password, false)).Succeeded)
                    {
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };
                        var guid = config["Auth:GUID"];
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(guid));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: config["Auth:Token:Issuer"],
                            audience: config["Auth:Token:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMonths(1),
                            signingCredentials: creds
                            );

                        
                        
                        return Json(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            role = userMgr.GetRolesAsync(user).Result,
                            currentUser = user
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception thrown while creating a JWT: {ex}");
            }

            return BadRequest("Failed to generate token.");
        }
        

        [HttpPost("newuser")]
        public async Task<IActionResult> NewUser([FromBody] CredentialModel model)
        {

            if (model == null)
            {
                return BadRequest($"The request body can't be null {model}");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var role = model.RoleClaim?.ToLower();
            if (string.IsNullOrEmpty(role))
            {
                return BadRequest();
            }

            var user = await userMgr.FindByNameAsync(model.UserName);

            if (user == null)
            {
                var newUser = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name
                    
                };

                var result = await userMgr.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {

                    newUser = ctx.Users.Where(u => u.UserName == newUser.UserName).FirstOrDefault();

                    var roleResult = await userMgr
                        .AddToRoleAsync(newUser, model.RoleClaim);

                    return Created($"api/auth/login", result);
                }
                else
                {
                    await userMgr.DeleteAsync(newUser);
                    return BadRequest();
                }
            }

            ///TODO: Add extra checks for correct provided information

            return BadRequest("The user might already exist.");
        }
    }
}
