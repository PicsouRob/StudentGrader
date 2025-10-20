using Domain.Interfaces;
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
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
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
            var existedStudent = await _authService.LoginAsync(model.Email, model.Password, model.RememberMe);

            return RedirectToAction("Index", "Home");
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
        await _authService.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}

