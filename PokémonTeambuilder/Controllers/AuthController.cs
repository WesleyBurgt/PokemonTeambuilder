using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserService userService;

    public AuthController(IConfiguration configuration, PokemonTeambuilderDbContext context)
    {
        _configuration = configuration;
        userService = new UserService(new UserRepos(context));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        User user;
        try
        {
            user = await userService.AuthenticateUserAsync(login.Username, login.Password);
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds
        );

        var refreshToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds
        );

        return Ok(new
        {
            accesToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken)
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginModel login)
    {
        User user;
        try
        {
            user = await userService.RegisterUserAsync(login.Username, login.Password);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds
        );

        var refreshToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds
        );

        return Ok(new
        {
            accesToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken)
        });
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
