using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tareas.Lib.Models
{
  public class TareaExtendido : Tarea
  {
    public string NombreProyecto { get; set; }
    public string UsuarioCrea { get; set; }
    public string TiempoEstimadoString { get; set; }
    public double? TiempoEjecutado { get; set; }    
  }
}
