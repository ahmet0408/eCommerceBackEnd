using eCommerce.bll.ViewModel;
using eCommerce.dal;
using eCommerce.dal.Data;
using eCommerce.dal.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.bll.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public UserService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IOptions<JWT> jwt)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _jwt = jwt.Value;
        }
        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.PhoneNumber,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var userWithSamePhoneNumber = await _userManager.FindByNameAsync(model.PhoneNumber);
            if (userWithSamePhoneNumber == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return $"User Registered with Phone Number {user.UserName}";
                }
                else
                {
                    return $"Simple Password";
                }
            }
            else
            {
                return $"Phone Number {user.PhoneNumber } is already registered.";
            }
        }
        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);
            if (user == null)
            {
                authenticationModel.Message = $"No Accounts Registered with {model.PhoneNumber}.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.PhoneNumber = user.PhoneNumber;                
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                //authenticationModel.Roles = rolesList.ToList();


                //if (user.RefreshTokens.Any(a => a.IsActive))
                //{
                //    var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                //    authenticationModel.RefreshToken = activeRefreshToken.Token;
                //    authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
                //}
                //else
                //{
                //    var refreshToken = CreateRefreshToken();
                //    authenticationModel.RefreshToken = refreshToken.Token;
                //    authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                //    user.RefreshTokens.Add(refreshToken);
                //    _context.Update(user);
                //    _context.SaveChanges();
                //}

                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName.ToLower()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.PhoneNumber),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }        
    }
}
