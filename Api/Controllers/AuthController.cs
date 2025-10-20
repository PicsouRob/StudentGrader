using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<Users> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<Users> userManager, IConfiguration configuration
    ){
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var findedUser = await _userManager.FindByEmailAsync(loginDto.Email);

        if (findedUser == null) return Unauthorized(new { Message = "No existe usuario con este correo." });

        var passwordCheckResult = await _userManager.CheckPasswordAsync(findedUser, loginDto.Password);

        if (!passwordCheckResult) return Unauthorized(new { Message = "Contraseña incorrecta." });

        var claims = new[]
        {
            new Claim(ClaimTypes.GivenName, findedUser.FullName ?? string.Empty),
            new Claim(ClaimTypes.Email, findedUser.Email ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, findedUser.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { Token = tokenString });
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existedUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existedUser != null) return BadRequest(new { Message = "Ya existe un usuario con ese correo." });

        var user = new Users { Email = registerDto.Email, FullName = registerDto.Name };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok(new { Message = "Usuario registrado con exito." });
    }
    
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout([FromBody] Users user)
    {
        await _userManager.DeleteAsync(user);

        return Ok(new { Message = "Sesión cerrada exitosamente." });
    }
}

