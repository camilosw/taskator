using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Tareas.Lib.Validation;

namespace Tareas.Lib.Models
{
    public class UsuarioExtendido : Usuario
    {
        public ICollection<TareaExtendido> Tareas { get; set; }
    }
}
