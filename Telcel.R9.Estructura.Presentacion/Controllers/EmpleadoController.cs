using Microsoft.AspNetCore.Mvc;

namespace Telcel.R9.Estructura.Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            Negocio.Empleado empleado = new Negocio.Empleado();
            empleado.Nombre = "";

            Negocio.Result result = Negocio.Empleado.GetAll();
            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al mostrar la Vista" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }
        //[HttpPost]
        //public ActionResult GetAll(Negocio.Empleado empleado)
        //{
        //    if (empleado.Nombre == null)
        //    {
        //        empleado.Nombre = "";
        //    }
        //    if (empleado.ApellidoPaterno == null)
        //    {
        //        empleado.ApellidoPaterno = "";
        //    }
        //    if (empleado.ApellidoMaterno == null)
        //    {
        //        empleado.ApellidoMaterno = "";
        //    }
        //    Negocio.Result result = Negocio.Empleado.GetAll(empleado);
        //    if (result.Correct)
        //    {
        //        empleado.Empleados = result.Objects;
        //        return View(empleado);
        //    }
        //    else
        //    {
        //        ViewBag.Mensaje = "Ocurrio un error al mostrar la Vista" + result.ErrorMessage;
        //        return PartialView("Modal");
        //    }
        //}
        [HttpGet]
        public ActionResult Form()
        {
            Negocio.Empleado empleado = new Negocio.Empleado();
            empleado.Puesto = new Negocio.Puesto();
            empleado.Departamento = new Negocio.Departamento();

            Negocio.Result resultDepartamento = Negocio.Departamento.GetAll();
            if (resultDepartamento.Correct)
            {
                empleado.Departamento.Departamentos = resultDepartamento.Objects;
                Negocio.Result resultPuesto = Negocio.Puesto.GetAll();
                if (resultPuesto.Correct)
                {
                    empleado.Puesto.Puestos = resultPuesto.Objects;
                    return View(empleado);
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al cargar la vista";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al cargar la vista";
                return PartialView("Modal");
            }

        }
        [HttpPost]
        public ActionResult Form(Negocio.Empleado empleado)
        {
            Negocio.Result result = Negocio.Empleado.Add(empleado);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Empleado agregado exitosamente";
            }
            else
            {
                ViewBag.Mensaje = "El empleado no se agrego exitosamente" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdEmpleado)
        {
            Negocio.Result result = Negocio.Empleado.Delete(IdEmpleado);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Empleado eliminado exitosamente";
            }
            else
            {
                ViewBag.Mensaje = "El empleado no se elimino exitosamente" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
    }
}
