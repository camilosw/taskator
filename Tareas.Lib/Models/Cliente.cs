using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace Tareas.Lib.Models
{
  [TableName("cliente")]
  [PrimaryKey("id")]
  public class Cliente
  {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Activo { get; set; }
  }
}
