using System;
using System.Collections.Generic;

namespace Telcel.R9.Estructura.AccesoDatos
{
    public partial class Departamento
    {
        public Departamento()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int DepartamentoId { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
