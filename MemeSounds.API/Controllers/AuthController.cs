using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MemeSounds.API.Data;
using MemeSounds.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MemeSounds.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private UserManager<ApplicationUser> userManager;

    public AuthController(UserManager<ApplicationUser> userManager)
    {
      this.userManager = userManager;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
      var user = await userManager.FindByNameAsync(model.Username);
      if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
      {
        var claims = new[]
        {
          new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

        var token = new JwtSecurityToken(
          issuer: "http://oec.com",
          audience: "http://oec.com",
          expires: DateTime.UtcNow.AddHours(1),
          claims: claims,
          signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        );

        return Ok ( new {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
      }

      return Unauthorized();

      
    }

  }
}