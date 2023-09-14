using APIAuth.Models;
using APIAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIAuth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly TokenService _tokenService;


    public AuthController(ILogger<AuthController> logger, TokenService tokenService)
    {
        _logger = logger;
        _tokenService = tokenService;
    }
    
    [HttpPost("token")]
    public IActionResult Token([FromBody] UserViewModel userViewModel)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid Request");

        if (!_tokenService.IsAuthenticated(userViewModel, out var token))
            return Unauthorized();

        return Ok(token);
    }
}