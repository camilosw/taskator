using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tareas.Lib.Models;

namespace Tareas.Lib.Services
{
  public class UsuarioService : IUsuarioService
  {
    public UsuarioService()
    {
        db = new Database("Tareas");
    }

    public List<Usuario> GetAll()
    {
        var sql = Sql.Builder.Select("*").From(tabla).OrderBy("nombre");
        return db.Query<Usuario>(sql).ToList();
    }

    public List<Usuario> GetAllActivos()
    {
        var sql = Sql.Builder.Select("*").From(tabla).Where("activo = 1").OrderBy("nombre");
        return db.Query<Usuario>(sql).ToList();
    }

    public List<KeyValuePair<string, string>> GetIdValueList()
    {
        var sql = Sql.Builder.Select("id, nombre, apellido").From(tabla).OrderBy("nombre");
        var results = db.Query<Usuario>(sql);

        List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
        foreach (var item in results)
        {
            string nombre = item.Nombre +
                (String.IsNullOrWhiteSpace(item.Apellido) ? "" : " " + item.Apellido);
            list.Add(new KeyValuePair<string, string>(item.Id.ToString(), nombre));
        }
        return list;
    }

    public List<UsuarioExtendido> GetConTareas()
    {
        var sqlUsuarios = Sql.Builder.Select("*")
            .From(tabla)
            .Where("usuario.activo = 1")
            .OrderBy("nombre_usuario");

        var usuarios = db.Query<UsuarioExtendido>(sqlUsuarios).ToList();

        var sqlTareas = Sql.Builder
            .Select("tarea.*, proyecto.nombre as NombreProyecto, " +
                    "(select nombre_usuario from usuario where usuario.id = tarea.id_usuario_crea) as UsuarioCrea," +
                    "sum(log_trabajo_tareas.minutos) as TiempoEjecutado")
            .From("tarea")
            .LeftJoin("usuario").On("usuario.id = tarea.id_asignado")
            .LeftJoin("estado").On("estado.id = tarea.id_estado")
            .LeftJoin("proyecto").On("proyecto.id = tarea.id_proyecto")
            .LeftJoin("log_trabajo_tareas").On("log_trabajo_tareas.id_tarea = tarea.id")
            .Where("proyecto.activo = 1 and estado.tarea_activa = 1")
            .GroupBy("tarea.id")
            .OrderBy("usuario.nombre_usuario, ISNULL(tarea.fecha_entrega)," +
                      "tarea.fecha_entrega, proyecto.orden, tarea.orden");

        var tareas = db.Query<TareaExtendido>(sqlTareas).ToList();

        foreach (UsuarioExtendido usuario in usuarios)
        {
            usuario.Tareas = (from t in tareas
                              where t.IdAsignado == usuario.Id
                              select t).ToList();
        }

        return usuarios;
    }

    public Usuario GetById(int id)
    {
        var sql = Sql.Builder.Where("Id = @0", id);
        return db.SingleOrDefault<Usuario>(sql);
    }

    public Usuario GetByNombreUsuario(string nombreUsuario)
    {
        var sql = Sql.Builder.Where("nombre_usuario = @0", nombreUsuario);
        return db.SingleOrDefault<Usuario>(sql);
    }

    public void Insert(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void Update(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    private Database db;
    private const string tabla = "usuario";
  }

  public interface IUsuarioService
  {
    List<Usuario> GetAll();
    List<Usuario> GetAllActivos();
    List<KeyValuePair<string, string>> GetIdValueList();
    List<UsuarioExtendido> GetConTareas();

    Usuario GetById(int id);
    Usuario GetByNombreUsuario(string nombreUsuario);
    void Insert(Usuario usuario);
    void Update(Usuario usuario);
    void Delete(int id);
  }
}
