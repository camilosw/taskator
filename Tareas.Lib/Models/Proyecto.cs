using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace Tareas.Lib.Models
{
  [TableName("proyecto")]
  [PrimaryKey("id")]
  public class Proyecto
  {
    public int Id { get; set; }
    [Column("id_cliente")]
    public int? IdCliente { get; set; }
    public string Nombre { get; set; }
    [Column("codigo_presupuesto")]
    public string CodigoPresupuesto { get; set; }
    public int Orden { get; set; }
    public int Activo { get; set; }
    public DateTime Creado { get; set; }
    public DateTime Actualizado { get; set; }
  }
}
