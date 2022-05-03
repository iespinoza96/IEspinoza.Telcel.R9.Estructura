using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Usuario
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public static Result GetByUserNameEF(string userName)
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.IEspinozaTelcelR9EstructuraContext context = new AccesoDatos.IEspinozaTelcelR9EstructuraContext())
                {
                    var objUsuario = context.Usuarios.FromSqlRaw($"GetByUserName {userName}").AsEnumerable().FirstOrDefault();

                    if (objUsuario != null)
                    {
                        Usuario usuario = new Usuario();
                        usuario.UserName = objUsuario.UserName;
                        usuario.Password = objUsuario.Password;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
