using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Puesto
    {
        public int PuestoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Puestos { get; set; }
        public static Result GetAll()
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.IEspinozaTelcelR9EstructuraContext context = new AccesoDatos.IEspinozaTelcelR9EstructuraContext())
                {
                    var puestos = context.Puestos.FromSqlRaw("$PuestotoGetAll").ToList();
                    if (puestos != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objPuesto in puestos)
                        {
                            Puesto puesto = new Puesto();
                            puesto.PuestoID = objPuesto.PuestoId;
                            puesto.Descripcion = objPuesto.Descripcion;

                            result.Objects.Add(puesto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Puesto";
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
