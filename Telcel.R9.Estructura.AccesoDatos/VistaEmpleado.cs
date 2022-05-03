using System;
using System.Collections.Generic;

namespace Telcel.R9.Estructura.AccesoDatos
{
    public partial class VistaEmpleado
    {
        public int EmpleadoId { get; set; }
        public string? Nombre { get; set; }
        public int PuestoId { get; set; }
        public string? Puesto { get; set; }
        public int DepartamentoId { get; set; }
        public string? Departamento { get; set; }
    }
}
