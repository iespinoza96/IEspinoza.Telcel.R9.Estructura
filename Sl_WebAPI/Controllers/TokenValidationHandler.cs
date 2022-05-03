using Microsoft.IdentityModel.Tokens;
using System.Net;
//using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace Sl_WebAPI.Controllers
{
    public class TokenValidationHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        //private readonly IHostingEnvironment _hostingEnvironment;

        public TokenValidationHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            //_hostingEnvironment = hostingEnvironment;
        } //inyección de dependencias 
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            // determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var secretKey = _configuration["JWT_SECRET_KEY:value"];
                var audienceToken = _configuration["JWT_AUDIENCE_TOKEN:value"];
                var issuerToken = _configuration["JWT_ISSUER_TOKEN:value"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                // Extract and assign Current Principal and user
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                //HttpContext.Equals.User = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }
        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}
