using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace Tareas.Lib.Models
{
  public class ProyectoExtendido : Proyecto
  {
    public ICollection<TareaExtendido> Tareas { get; set; }
  }
}
