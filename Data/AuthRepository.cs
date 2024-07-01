using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Dtos.User;
using Auth.Models;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Data{
    public class AuthRepository : IAuthRepository{
        
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(UserRegisterDto user, string? error)> Register(UserRegisterDto user) {
            var existingUser = await _context.User.SingleOrDefaultAsync(x => x.Username == user.Username);
            if (existingUser != null) {
                return (null, "Username already exists")!;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _context.User.AddAsync(new User
                { Username = user.Username, Password = user.Password, Role = user.Role });
            await _context.SaveChangesAsync();
            var  token = GenerateJwtToken(new User
                {Username = user.Username, Password = user.Password, Role = user.Role});
            return (new UserRegisterDto { Username = user.Username,  Role = user.Role }, null);
        }

        [Obsolete("Obsolete")]
        public async Task<LoginResponseDto> Login(string username, string password) {
            var user = await _context.User.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null) {
                return new LoginResponseDto { Success = false, Message = "User not found" };
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) {
                return new LoginResponseDto { Success = false, Message = "Incorrect password" };
            }

            var token = GenerateJwtToken(user);
            return new LoginResponseDto { Token = token, Success = true };
        }


        public string GenerateJwtToken(User user) {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);
            if (key.Length * 8 < 128) {
                var newKey = new byte[16];
                Array.Copy(key, newKey, Math.Min(key.Length, newKey.Length));
                key = newKey;
            }

            var signingKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Issuer = "Me",
                Audience = "You",
                Expires = DateTime.Now.AddDays(1),
                TokenType = "Bearer",
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}