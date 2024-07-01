using Auth.Data;
using Auth.Dtos.User;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase{
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepo) {
        _authRepo = authRepo;
    }
    [AllowAnonymous]
    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userLoginDto) {
        var result = await _authRepo.Register(userLoginDto); 
        if (result.error != null) return BadRequest(result.error);
        return Ok(result.user);
        
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponseDto>> Login(UserLoginDto request) {
        var response = await _authRepo.Login(request.Username, request.Password);
        if (!response.Success) {
            return BadRequest(new LoginResponseDto { Success = false, Message = response.Message });
        }
    
        return Ok(new LoginResponseDto { Token = response.Token, Success = true });
    }
    
}