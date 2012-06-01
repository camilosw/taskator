using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace Tareas.Lib.Models
{
  [TableName("tarea")]
  [PrimaryKey("id")]
  public class Tarea
  {
    public int Id { get; set; }
    [Column("id_proyecto")]
    public int IdProyecto { get; set; }
    [Column("id_estado")]
    public int IdEstado { get; set; }
    [Column("id_asignado")]
    public int? IdAsignado { get; set; }
    [Column("id_usuario_crea")]
    public int? IdUsuarioCrea { get; set; }
    public string Descripcion { get; set; }
    [Column("fecha_entrega")]
    public DateTime? FechaEntrega { get; set; }
    [Column("tiempo_estimado")]
    public int? TiempoEstimado { get; set; }   // El tiempo estimado es en minutos
    public int Orden { get; set; }
    public DateTime Creada { get; set; }
    public DateTime Actualizada { get; set; }
  }
}
