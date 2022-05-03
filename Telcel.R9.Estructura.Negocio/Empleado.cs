using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }

        public string Nombre { get; set; }

        public Puesto Puesto { get; set; }

        public Departamento Departamento { get; set; }

        public List<object> Empleados { get; set; }
        public static Result GetAll()
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.IEspinozaTelcelR9EstructuraContext context = new AccesoDatos.IEspinozaTelcelR9EstructuraContext())
                {
                    var empleados = context.Empleados.FromSqlRaw("EmpleadoGetAll").ToList();//EmpleadoGetAll(empleadoBusqueda.Nombre, empleadoBusqueda.ApellidoPaterno, empleadoBusqueda.ApellidoMaterno).ToList();
                    if (empleados != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objEmpleado in empleados)
                        {
                            Empleado empleado = new Empleado();
                            empleado.EmpleadoID = objEmpleado.EmpleadoId;
                            empleado.Nombre = objEmpleado.Nombre;
                            empleado.Puesto = new Puesto();
                            empleado.Puesto.PuestoID = objEmpleado.PuestoId.Value;
                            empleado.Puesto.Descripcion = objEmpleado.NombrePuesto;
                            empleado.Departamento = new Departamento();
                            empleado.Departamento.DepartamentoID = objEmpleado.DepartamentoId.Value;
                            empleado.Departamento.Descripcion = objEmpleado.NombreDepartamento;

                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Empleado";
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
        public static Result Add(Empleado empleado)
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.IEspinozaTelcelR9EstructuraContext context = new AccesoDatos.IEspinozaTelcelR9EstructuraContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.Nombre}', '{empleado.Puesto.PuestoID}', '{empleado.Departamento.DepartamentoID}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al insertar el registro en la tabla Empleado";
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
        public static Result Delete(int IdEmpleado)
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.IEspinozaTelcelR9EstructuraContext context = new AccesoDatos.IEspinozaTelcelR9EstructuraContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete {IdEmpleado}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al eliminar el registro";
                    }
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return (result);
        }

    }
}
