using System.Security.Claims;
using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View(new RegisterDto());
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var existedStudent = await _authService.RegisterAsync(model.Name, model.Email, model.Password);

            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            ViewData["AuthError"] = ex.Message;

            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Login()
    {
        if(User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View(new LoginDto());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var existedStudent = await _authService.LoginAsync(model.Email, model.Password);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existedStudent.Name!),
                new Claim(ClaimTypes.NameIdentifier, existedStudent.Id.ToString()),
                new Claim(ClaimTypes.Email, existedStudent.Email!),
            };

            var identity = new ClaimsIdentity(claims, "AppCookie");
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(3),
            };

            await HttpContext.SignInAsync("AppCookie", principal, authProperties);

            return RedirectToAction("Index", "Dashboard");
        }
        catch (Exception ex)
        {
            ViewData["AuthError"] = ex.Message;

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("AppCookie");

        return RedirectToAction("Index", "Home");
    }
}

