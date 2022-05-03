using System;
using System.Collections.Generic;

namespace Telcel.R9.Estructura.AccesoDatos
{
    public partial class Puesto
    {
        public Puesto()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int PuestoId { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
