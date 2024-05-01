using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Webapi.Controllers;
using Webapi.Models;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly JwtToken configuration;
    public TokenController(ApiContext _context, JwtToken configuration)
    {
        this._context = _context;
        this.configuration = configuration;
    }

   [HttpPost]
    public IActionResult GenerateToken(Userlogin details)
    {
        string password = string.Empty;
         password = Hashing.HashSHA(details.Password);
        details.Password = password;
        if (details.Password == null || details.Email == null)
        {
            return Unauthorized();
        }
        var user = _context.Registers.Where(u => u.Email == details.Email && u.Password == details.Password).FirstOrDefault();
        if (user != null)
        {
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, details.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiredOn = DateTime.Now.AddMinutes(configuration.TokenExpirationTime);
        var token = new JwtSecurityToken(configuration.ValidIssuer,
              configuration.ValidAudience,
              claims,
              expires: expiredOn,
              signingCredentials: creds);
       
        return Ok(new
        {
            ExpireOnDate = token.ValidTo,
            Success = true,
            ExpiryIn = configuration.TokenExpirationTime,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}