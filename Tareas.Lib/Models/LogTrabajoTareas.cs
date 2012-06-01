using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace Tareas.Lib.Models
{
  [TableName("log_trabajo_tareas")]
  [PrimaryKey("id")]
  public class LogTrabajoTareas
  {
    public int Id { get; set; }
    [Column("id_tarea")]
    public int IdTarea { get; set; }
    [Column("id_persona")]
    public int IdPersona { get; set; }
    [Column("fecha_inicio")]
    public DateTime FechaInicio { get; set; }
    [Column("fecha_fin")]
    public DateTime FechaFin { get; set; }
    public float Minutos { get; set; }
  }
}
