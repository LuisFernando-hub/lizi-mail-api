using lizi_mail_api.DTOs;
using lizi_mail_api.Entities;
using lizi_mail_api.Infra.Repository.User;
using lizi_mail_api.Request.Auth;
using lizi_mail_api.Response;
using lizi_mail_api.Services.ApiKey;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace lizi_mail_api.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IApiKeyService _apiKeyService;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IApiKeyService apiKeyService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _apiKeyService = apiKeyService;
            _configuration = configuration;
        }
        public async Task<Result<bool>> create(CreateUserRequest request)
        {

            var checkUser = await _userRepository.getByEmail(request.email);

            if (checkUser != null)
            {
                return Result<bool>.error(false, "The User already exists.");
            }

            var hashedPassword = new PasswordHasher<UserEntity>().HashPassword(null, request.password);

            var user = new UserEntity(request.name, request.email, hashedPassword);

            try
            {
                await _userRepository.create(user);
                var result = await _apiKeyService.createForUser(user.id.ToString());
                if (!result.status)
                {
                    return Result<bool>.error(false, result.message ?? "Erro create api key for user");
                }
                await _userRepository.commitAsync();
                return Result<bool>.success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.error(false, ex.Message);
            }
        }

        public async Task<Result<AuthLoginDTO>> login(LoginRequest request)
        {
            var user = await _userRepository.getByEmail(request.email);
            if (user == null)
            {
                return Result<AuthLoginDTO>.error(false, "The User does not exist.");
            }

            var verifyPassword = new PasswordHasher<UserEntity>().VerifyHashedPassword(user, user.password_hash, request.password);

            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                return Result<AuthLoginDTO>.error(false, "Invalid password.");
            }

            if (user.is_active == false)
            {
                return Result<AuthLoginDTO>.error(false, "The User is not active.");
            }

            string token = CreateToken(user);

            //TODO Mapper
            var apiKey = await _apiKeyService.getActiveByUserId(user.id.ToString());

            if (!apiKey.status)
            {
                return Result<AuthLoginDTO>.error(false, "The ApiKey is not found.");
            }

            var responseDto = new AuthLoginDTO(token, user.name, user.email, apiKey.data.key_hash);
            
            return Result<AuthLoginDTO>.success(responseDto);
        }

        public string CreateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task<Result<bool>> getByEmail(string email)
        {
            var result = await _userRepository.getByEmail(email);

            if (result == null)
            {
                return Result<bool>.error(false, "The User not found");
            }

            return Result<bool>.success(true);
        }
    }
}
