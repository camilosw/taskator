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
    [TableName("usuario")]
    [PrimaryKey("Id")]
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DisplayName("Nombre de usuario")]
        [Column("nombre_usuario")]
        public string NombreUsuario { get; set; }
        
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        //public string Clave { get; set; }

        //[DisplayName("Requiere cambio de clave")]
        //public bool RequiereCambioClave { get; set; }

        //[Required(ErrorMessage = "Requerido")]
        //[Email(ErrorMessage = "No es un correo válido")]
        //public string Email { get; set; }

        public bool Activo { get; set; }

        //public DateTime FechaCreacion { get; set; }

        [DisplayName("Administrador del sistema")]
        public bool Administrador { get; set; }
    }
}
