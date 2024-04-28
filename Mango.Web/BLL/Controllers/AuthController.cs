using Mango.Web.BLL.Services.IServices;
using Mango.Web.BLL.Utilities;
using Mango.Web.DAL.Models.Dto.Request;
using Mango.Web.DAL.Models.Dto.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.BLL.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View(new LoginRequestDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            var responseDto = await _authService.LoginAsync(model);

            if (responseDto == null || !responseDto.IsSuccess)
            {
                ModelState.AddModelError("CustomError", responseDto.Message);
                return View(model);
            }

            var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

            await SignInUser(loginResponse);
            _tokenProvider.SetToken(loginResponse.Token);

            TempData["success"] = "Login Successful";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem(text: SD.RoleAdmin, value: SD.RoleAdmin),
                new SelectListItem(text: SD.RoleCustomer, value: SD.RoleCustomer)
            };

            ViewBag.RoleList = roleList;
            return View(new RegistrationRequestDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto model)
        {
            var result = await _authService.RegisterAsync(model);
            var assignRole = await _authService.AssignRoleAsync(new RoleAssignRequestDto() { Email = model.Email, RoleName = model.RoleName ?? SD.RoleCustomer });

            if (result == null || !result.IsSuccess || assignRole == null || !assignRole.IsSuccess)
            {
                var roleList = new List<SelectListItem>()
                {
                    new SelectListItem(text: SD.RoleAdmin, value: SD.RoleAdmin),
                    new SelectListItem(text: SD.RoleCustomer, value: SD.RoleCustomer)
                };

                ModelState.AddModelError("CustomError", result.Message);
                ViewBag.RoleList = roleList;
                return View(model);
            }

            TempData["success"] = "Registration Successful";
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();

            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto modal)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(modal.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault( u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
