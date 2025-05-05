using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.Helpers;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.IRepository;
using SoccerKFUPM.Infrastructure.Repository;
using System.Net;

namespace SoccerKFUPM.Application.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private UserManager<User> _userManager;
        private JwtSettings _jwtSettings;
        private SignInManager<User> _signInManager;
        private IPersonRepository _personRepository;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationServices(UserManager<User> userManager, IOptions<JwtSettings> jwtSettings, SignInManager<User> signInManager, IPersonRepository personRepository, RoleManager<IdentityRole<int>> roleManager, IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _personRepository = personRepository;
            _roleManager = roleManager;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Result<AuthenticationResponseDTO>> AuthenticateUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Result<AuthenticationResponseDTO>.Failure(Error.UnauthorizedError("Invalid credentials"), System.Net.HttpStatusCode.Unauthorized);


            var IsCredentialsValied = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!IsCredentialsValied.Succeeded)
                return Result<AuthenticationResponseDTO>.Failure(Error.UnauthorizedError("Invalid credentials"), System.Net.HttpStatusCode.Unauthorized);


            // get user Roles
            var roles = await _userManager.GetRolesAsync(user);



            var newToken = AuthHelpers.GenerateJwtToken(user, _jwtSettings, roles.ToList());

            var newRefreshToken = AuthHelpers.GetRefreshToken();

            DateTime RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);

            await _refreshTokenRepository.AddRefreshTokenAsync(new RefreshToken()
            {
                CreatedAt = DateTime.UtcNow,
                ExpiryTime = RefreshTokenExpiryTime,
                Token = newRefreshToken,
                IsRevoked = false,
                UserId = user.Id,

            });



            return Result<AuthenticationResponseDTO>.Success(new AuthenticationResponseDTO
            {
                UserId = user.Id,
                AuthenticationMessage = "Authenticated successfully",
                JWTToken = newToken,
                RefreshToken = newRefreshToken,
                Roles = roles.ToList(),
            });
        }

        public async Task<Result<AuthenticationResponseDTO>> RefreshTokenAsync(string refreshToken)
        {
            (User? user, bool result) = await _refreshTokenRepository.CheckRefreshTokenIsValidAsync(refreshToken);

            if (!result)
                return Result<AuthenticationResponseDTO>.Failure(Error.UnauthorizedError("Invalid or expired refresh token."), System.Net.HttpStatusCode.Unauthorized);

            // get user Roles
            var roles = await _userManager.GetRolesAsync(user);



            string newToken = AuthHelpers.GenerateJwtToken(user, _jwtSettings, roles.ToList());
            string newRefreshToken = AuthHelpers.GetRefreshToken();
            DateTime RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
            //save new refreshToken
            await _refreshTokenRepository.AddRefreshTokenAsync(new RefreshToken()
            {
                CreatedAt = DateTime.UtcNow,
                ExpiryTime = RefreshTokenExpiryTime,
                Token = newRefreshToken,
                IsRevoked = false,
                UserId = user.Id,

            });


            return Result<AuthenticationResponseDTO>.Success(new AuthenticationResponseDTO
            {
                UserId = user.Id,
                AuthenticationMessage = "Authenticated successfully",
                JWTToken = newToken,
                RefreshToken = newRefreshToken,
                Roles = roles.ToList(),
            });

        }

        public async Task<Result<AuthenticationResponseDTO>> RegisterUserAsync(RegsterAccountRequestDTO registerUserDTO)
        {
            // ✅ 1. Check if the person (KFUPMId) already exists
            bool personExists = await _personRepository.CheckIsPersonExistAsync(registerUserDTO.KFUPMId);

            if (personExists)
            {
                return Result<AuthenticationResponseDTO>.Failure(
                    Error.ValidationError("This person already exists."),
                    HttpStatusCode.Conflict
                );
            }

            // ✅ 2. Create new user and associated person entity
            var newUser = new User
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.UserName,
                Person = new Person
                {
                    KFUPMId = registerUserDTO.KFUPMId,
                    FirstName = registerUserDTO.FirstName,
                    SecondName = registerUserDTO.SecondName,
                    ThirdName = registerUserDTO.ThirdName,
                    LastName = registerUserDTO.LastName
                }
            };

            var userCreationResult = await _userManager.CreateAsync(newUser, registerUserDTO.Password);
            if (!userCreationResult.Succeeded)
            {
                return Result<AuthenticationResponseDTO>.Failure(
                    Error.ValidationError("User creation failed: " + string.Join(", ", userCreationResult.Errors.Select(e => e.Description))),
                    HttpStatusCode.BadRequest
                );
            }

            // ✅ 3. Assign the default role (Guest)
            var roleAssignmentResult = await _userManager.AddToRoleAsync(newUser, "Gust");
            if (!roleAssignmentResult.Succeeded)
            {
                return Result<AuthenticationResponseDTO>.Failure(
                    Error.ValidationError("Failed to assign role: " + string.Join(", ", roleAssignmentResult.Errors.Select(e => e.Description))),
                    HttpStatusCode.BadRequest
                );
            }

            // get user Roles
            var roles = await _userManager.GetRolesAsync(newUser);


            // ✅ 4. Generate JWT and Refresh Token
            string jwtToken = AuthHelpers.GenerateJwtToken(newUser, _jwtSettings, roles.ToList());
            string refreshToken = AuthHelpers.GetRefreshToken();
            DateTime refreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);

            // ✅ 5. Save Refresh Token
            var refreshTokenEntity = new RefreshToken
            {
                CreatedAt = DateTime.UtcNow,
                ExpiryTime = refreshTokenExpiry,
                Token = refreshToken,
                IsRevoked = false,
                UserId = newUser.Id
            };
            bool result = await _refreshTokenRepository.AddRefreshTokenAsync(refreshTokenEntity);

            // ✅ 6. Return authentication response
            return Result<AuthenticationResponseDTO>.Success(new AuthenticationResponseDTO
            {
                UserId = newUser.Id,
                AuthenticationMessage = "Authenticated successfully.",
                JWTToken = jwtToken,
                RefreshToken = refreshToken,
                Roles = ["GUST"]

            });
        }

        public async Task<Result<List<RoleDTO>>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var result = roles.Select(r => new RoleDTO
            {
                Id = r.Id.ToString(),
                Name = r.Name!,
                NormalizedName = r.NormalizedName!
            }).ToList();

            return Result<List<RoleDTO>>.Success(result);
        }
    }
}

