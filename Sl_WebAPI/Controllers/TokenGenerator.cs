using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
//using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace Sl_WebAPI.Controllers
{
    public class TokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator()
        {
        }

        // private readonly IHostingEnvironment _hostingEnvironment;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
            //_hostingEnvironment = hostingEnvironment;
        } //inyección de dependencias 
        public string GenerateTokenJwt(string userName)
        {
            // appsetting for Token JWT
            var secretKey = _configuration["JWT_SECRET_KEY:value"];
            var audienceToken = _configuration["JWT_AUDIENCE_TOKEN:value"];
            var issuerToken = _configuration["JWT_ISSUER_TOKEN:value"];
            var expireTime = _configuration["JWT_EXPIRE_MINUTES:value"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}
