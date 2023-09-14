using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIAuth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace APIAuth.Services;

public class TokenService
{
    private readonly TokenManagement _tokenManagement;
    private readonly UserService _userService;

    public TokenService(IOptions<TokenManagement> tokenManagement, UserService userService)
    {
        _tokenManagement = tokenManagement.Value;
        _userService = userService;
    }
    
    public bool IsAuthenticated(UserViewModel userViewModel, out string token)
    {
        token = string.Empty;

        if (!_userService.ValidateUser(userViewModel)) return false;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userViewModel.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwtToken = new JwtSecurityToken(
            _tokenManagement.Issuer,
            _tokenManagement.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
            signingCredentials: credentials
        );

        token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return true;
    }
}