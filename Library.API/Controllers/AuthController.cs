using Library.API.Filters;
using Library.API.Models;
using Library.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    public class AuthController : Controller
    {
        private LibraryDbContext ctx;
        private SignInManager<LibraryUser> signInMgr;
        private UserManager<LibraryUser> userMgr;
        private IPasswordHasher<LibraryUser> passwordHasher;
        private IConfigurationRoot config;

        public AuthController(LibraryDbContext context, SignInManager<LibraryUser> signInManager, UserManager<LibraryUser> userManager, IPasswordHasher<LibraryUser> hasher,
            IConfigurationRoot config)
        {
            ctx = context;
            signInMgr = signInManager;
            userMgr = userManager;
            passwordHasher = hasher;
            this.config = config;
        }

        [ValidateModel]
        [HttpPost("api/auth/login")]
        public async Task<IActionResult> Login ([FromBody] CredentialModel model)
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
        [HttpPost("api/auth/token")]
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

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo

                        });
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception thrown while creating a JWT: {ex}");
            }

            return BadRequest("Failed to generate token.");
        }
    }
}
