using LayuiAdminNetCore.Appsettings;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetGate.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LayuiAdminNetGate.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IOptions<AppSettings> _appSettings;
        public AuthenticateService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public bool IsAuthenticated(Authenticate request, out string assessToken)
        {
            assessToken = string.Empty;

            try
            {
                var claims = new[]
                {
                    new Claim(Policys.UId,Convert.ToString(request.UId)!),
                    new Claim(Policys.Phone,request.Phone!),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.JwtBearer!.SecurityKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //生成accessToken
                var jwtAssessToken = new JwtSecurityToken(
                _appSettings.Value.JwtBearer.Issuer,
                    _appSettings.Value.JwtBearer.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(_appSettings.Value.JwtBearer.AccessExpiration),
                    //expires: DateTime.Now.AddSeconds(10),
                    signingCredentials: credentials);
                assessToken = new JwtSecurityTokenHandler().WriteToken(jwtAssessToken);

                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}