using Auth.Dtos.User;
using Auth.Models;

namespace Auth.Data;

public interface IAuthRepository{
    Task<(UserRegisterDto user, string? error)> Register(UserRegisterDto user);

    Task<LoginResponseDto> Login(string username, string password);

}