using GetUrCourse.Services.AuthAPI.Constants;
using GetUrCourse.Services.AuthAPI.DTOs;
using GetUrCourse.Services.AuthAPI.Entities;
using GetUrCourse.Services.PaymentAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GetUrCourse.Contracts.User;

namespace GetUrCourse.Services.AuthAPI.Services
{
    /// <summary>
    /// Service for handling authentication, such as user registration and login.
    /// </summary>
    public class AuthService
    {
        private readonly AuthDbContext _authDbContext;
        private readonly string _token;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AuthDbContext authDbContext, IConfiguration configuration,
            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _authDbContext = authDbContext;
            _token = configuration.GetValue<string>("ApiSettings:Token");
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Registers a new user with registration data.
        /// </summary>
        /// <param name="model">The registration details of the user.</param>
        /// <returns>A registered user or an error message.</returns>
        public async Task<(ApplicationUser user, string errorMessage)> RegisterAsync(RegisterRequestDTO model)
        {
            var userFromDb = _authDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (userFromDb != null)
            {
                return (null, "Username already exists!");
            }

            var newUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.UserName,
                NormalizedEmail = model.UserName.ToUpper(),
                Name = model.Name
            };

            try
            {
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(AuthSettings.Role_Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(AuthSettings.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(AuthSettings.Role_Customer));
                    }

                    if (model.Role.ToLower() == AuthSettings.Role_Admin)
                    {
                        await _userManager.AddToRoleAsync(newUser, AuthSettings.Role_Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, AuthSettings.Role_Customer);
                    }

                    return (newUser, null);
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }

            return (null, "Error while registering!");
        }

        /// <summary>
        /// Authenticates a user based on their login data.
        /// </summary>
        /// <param name="model">The login details of the user.</param>
        /// <returns>A login response or an error message.</returns>
        public async Task<(LoginResponseDTO loginResponse, string errorMessage)> LoginAsync(LoginRequestDTO model)
        {
            var userFromDb = _authDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (userFromDb == null || !(await _userManager.CheckPasswordAsync(userFromDb, model.Password)))
            {
                return (null, "Username or Password is incorrect!");
            }

            var roles = await _userManager.GetRolesAsync(userFromDb);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_token);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("fullName", userFromDb.Name),
                    new Claim("id", userFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Email, userFromDb.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                }),
                // Token Expired in 7DAYS!
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            var loginResponse = new LoginResponseDTO()
            {
                Email = userFromDb.Email,
                Token = tokenHandler.WriteToken(token)
            };

            if (loginResponse.Email == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                return (null, "Username or Password is incorrect!");
            }

            return (loginResponse, null);
        }
        
        public async Task<(DeleteUserDTO, string errorMessage)> DeleteUserAsync(DeleteUserDTO model)
        {
            var userFromDb = _authDbContext.ApplicationUsers
                .FirstOrDefault(u => u.Id == model.UserId.ToString());

            if (userFromDb == null)
            {
                return (null, "User not found!");
            }

            if (userFromDb != null)
            {
                var result = await _userManager.DeleteAsync(userFromDb);
                if (result.Succeeded)
                {
                    return (model, null);
                }
            }

            return (null, "Error while deleting user!");
        }
    }
}