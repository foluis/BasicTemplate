﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NA.Template.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NA.Template.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost("[Action]", Name = nameof(Token))]
        public async Task<IActionResult> Token()
        {
            try
            {
                var header = Request.Headers["Authorization"];
                if (header.ToString().StartsWith("Basic"))
                {
                    var credvalue = header.ToString().Substring("basic".Length).Trim();
                    var userCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credvalue));
                    var userNameNpass = userCredentials.Split(':');

                    var result = await _signInManager.PasswordSignInAsync(userNameNpass[0], userNameNpass[1], isPersistent: true, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByEmailAsync(userNameNpass[0]);

                        if (user == null)
                        {
                            return NotFound();
                        }

                        var userClaim = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName)
                        };

                        foreach (var permission in user.Permissions)
                        {
                            userClaim.Add(new Claim(permission.Name, permission.Name));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("at_least_16_characters"));//yourKey (at least 16 characters) -> from application json or some secure else where
                        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: "http://www.domainname.com", //server - // ToDo: test other domains, make requests from diferents domains
                            audience: "http://www.domainname.com",//client - //ToDo: Get data from .json file
                            expires: DateTime.Now.AddMinutes(9) // Expiration minutes
                            , claims: userClaim//User information                            
                            , signingCredentials: credential
                            );

                        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(tokenString);
                    }
                    else
                    {
                        return StatusCode(500, "Upgrade exception");
                    }
                }
                return BadRequest("Username or pws not authorized");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString()); 
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost(Name = nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserModel user)
        {
            IdentityResult result = new IdentityResult();

            try
            {
                var applicationUser = new ApplicationUser { UserName = user.Email, Email = user.Email };
                result = await _userManager.CreateAsync(applicationUser, user.Password);
                if (result.Succeeded)
                {

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(applicationUser, isPersistent: false);

                    //return RedirectToLocal(returnUrl);
                    return CreatedAtRoute(nameof(CreateUser), new { id = user.Email }, user);
                }
                else
                {
                    return BadRequest(result.Errors);
                }

                //return CreatedAtRoute(nameof(GetById), new { id = clientDivision.Id, name = clientDivision.Name }, clientDivision);
            }
            catch (Exception)
            {
                _logger.LogError(ex.ToString()); 
                return StatusCode(500);
            }
        }
    }
}