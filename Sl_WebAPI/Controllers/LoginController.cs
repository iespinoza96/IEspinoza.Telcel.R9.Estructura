using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Telcel.R9.Estructura;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sl_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return (IHttpActionResult)Ok(true);
        }


        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return (IHttpActionResult)Ok("IPrincipal-user: " + identity.Name + "- IsAuthenticated: " + identity.IsAuthenticated);
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(Telcel.R9.Estructura.Negocio.Usuario usuario)
        {
            if (usuario == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                Telcel.R9.Estructura.Negocio.Result result = Telcel.R9.Estructura.Negocio.Usuario.GetByUserNameEF(usuario.UserName);
                if (result.Correct)
                {
                    if (((Telcel.R9.Estructura.Negocio.Usuario)result.Object).Password == usuario.Password)
                    {
                        
                        TokenGenerator tokenGenerator = new TokenGenerator();
                        var token = tokenGenerator.GenerateTokenJwt(usuario.UserName);
                        return (IHttpActionResult)Ok(token);
                    }
                    else
                    {
                        return (IHttpActionResult)Unauthorized();
                    }
                }
                else
                {
                    return (IHttpActionResult)Unauthorized(); //corregir
                }
            }

        }


    }
}
