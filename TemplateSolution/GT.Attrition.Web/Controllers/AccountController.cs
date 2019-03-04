using NA.Template.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
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

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

                    ////Escribir codigo para consultar usaurio y pws y poder comparar



                    if (userNameNpass[0] == "foluis@hotmail.com" && userNameNpass[1] == "mocoloco")
                    {
                        //database authorization
                        var userClaim = new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "useId"),
                            new Claim(ClaimTypes.Name, "Luis"),
                            //new Claim(ClaimTypes.Role, "Administrator"),
                            //new Claim("ClaimTypesPrueba", "RequireClaimPruebaValue"),
                            new Claim("UserCanReadSecurity","UserCanReadSecurity"),
                        };

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
                }
                return BadRequest("Username or pws not authorized");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString()); 
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
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString()); 
                return StatusCode(500);
            }
        }

        [Obsolete("Method1 is deprecated, please use Token instead.")]
        [HttpPost("[Action]", Name = nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = new Microsoft.AspNetCore.Identity.SignInResult();

            try
            {
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User logged in.");
                    //return RedirectToLocal(returnUrl);
                    return StatusCode(200, "Logged in");
                }
                if (result.RequiresTwoFactor)
                {
                    //return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                    return StatusCode(200, "LoginWith2fa");
                }
                if (result.IsLockedOut)
                {
                    //_logger.LogWarning("User account locked out.");
                    //return RedirectToAction(nameof(Lockout));
                    return StatusCode(200, "User account locked out");
                }
                else
                {
                    //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    //return View(model);
                    return BadRequest("Invalid login attempt.");
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString()); 
                return StatusCode(500, ex.Message);
            }
        }

        [Obsolete("Method1 is deprecated, pending kill token.")]
        [HttpPost("[Action]", Name = nameof(Logout))]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                //_logger.LogInformation("User logged out.");
                //return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString()); 
                return StatusCode(500, ex.Message);
            }

            return Ok("User logged out.");
        }


    }
}