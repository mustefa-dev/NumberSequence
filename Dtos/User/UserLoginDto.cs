namespace Auth.Dtos.User;

public class UserLoginDto{
    
public string Username { get; set; }=string.Empty;
    public string Password { get; set; }=string.Empty;
}   
public class LoginResponseDto {
    public string Token { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}
