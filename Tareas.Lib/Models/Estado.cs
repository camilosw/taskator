using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace Tareas.Lib.Models
{
    [TableName("estado")]
    [PrimaryKey("id")]
    public class Estado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        [Column("tarea_activa")]
        public int TareaActiva { get; set; }
    }
}
