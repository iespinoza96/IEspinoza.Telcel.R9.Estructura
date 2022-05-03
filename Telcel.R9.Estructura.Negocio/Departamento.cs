using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Departamentos { get; set; }
        public static Result GetAll()
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.IEspinozaTelcelR9EstructuraContext context = new AccesoDatos.IEspinozaTelcelR9EstructuraContext())
                {
                    var departamentos = context.Departamentos.FromSqlRaw("$DepartamentoGetAll").ToList();
                    if (departamentos != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objDepartamento in departamentos)
                        {
                            Departamento departamento = new Departamento();
                            departamento.DepartamentoID = objDepartamento.DepartamentoId;
                            departamento.Descripcion = objDepartamento.Descripcion;

                            result.Objects.Add(departamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Departamento";
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
