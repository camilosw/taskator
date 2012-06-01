using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tareas.Lib.Models;

namespace Tareas.Lib.Services
{
  public class ProyectoService : IProyectoService
  {
    public ProyectoService()
    {
        db = new Database("Tareas");
    }

    public List<Proyecto> GetAll()
    {
        var sql = Sql.Builder
            .Select("*")
            .From(tabla)
            .OrderBy("orden");

        return db.Query<Proyecto>(sql).ToList();
    }

    public List<ProyectoExtendido> GetVisibleConTareas()
    {
        var sqlProyectos = Sql.Builder.Select("proyecto.*")
            .From(tabla)
            .LeftJoin("cliente").On("cliente.id = proyecto.id_cliente")
            .Where("proyecto.activo = 1")
            .OrderBy("orden");

        var proyectos = db.Query<ProyectoExtendido>(sqlProyectos).ToList();

        var sqlTareas = Sql.Builder
            .Select("tarea.*, " +
                    "(select nombre_usuario from usuario where usuario.id = tarea.id_usuario_crea) as UsuarioCrea," +
                    "sum(log_trabajo_tareas.minutos) as TiempoEjecutado")
            .From("tarea")
            .LeftJoin("proyecto").On("proyecto.id = tarea.id_proyecto")
            .LeftJoin("estado").On("estado.id = tarea.id_estado")
            .LeftJoin("log_trabajo_tareas").On("log_trabajo_tareas.id_tarea = tarea.id")
            .Where("proyecto.activo = 1 and estado.tarea_activa = 1")
            .GroupBy("tarea.id")
            .OrderBy("proyecto.orden, ISNULL(tarea.fecha_entrega), tarea.fecha_entrega");

        var tareas = db.Query<TareaExtendido>(sqlTareas).ToList();

        foreach (ProyectoExtendido proyecto in proyectos)
        {
            proyecto.Tareas = (from t in tareas
                                where t.IdProyecto == proyecto.Id
                                select t).ToList();
        }
        return proyectos;
    }

    public Proyecto GetById(int id)
    {
        var sql = Sql.Builder.Where("id = @0", id);
        return db.SingleOrDefault<Proyecto>(sql);
    }

    public int Insert(Proyecto proyecto)
    {
        proyecto.Orden = GetMinProyectoOrden() - 1;
        proyecto.Activo = 1;
        proyecto.Creado = DateTime.Now;
        proyecto.Actualizado = proyecto.Creado;
        return Convert.ToInt32(db.Insert(proyecto));
    }

    public void Update(Proyecto proyecto)
    {
        proyecto.Actualizado = DateTime.Now;
        db.Update(proyecto);
    }

    public void UpdateList(List<int> idProyectos)
    {
        int i = 0;
        foreach (var id in idProyectos)
        {
            var proyecto = GetById(id);
            proyecto.Orden = i;
            proyecto.Actualizado = DateTime.Now;
            Update(proyecto);
            i++;
        }
    }

    public void Delete(int id)
    {
        db.Delete(tabla, "id", null, id);
    }

    private int GetMinProyectoOrden()
    {
        var sql = Sql.Builder.Select("min(orden)").From(tabla);
        int? lowestSortValue = db.Single<int?>(sql);
        return (lowestSortValue ?? 0);
    }

    private Database db;
    private const string tabla = "proyecto";
  }

  public interface IProyectoService
  {
    List<Proyecto> GetAll();
    List<ProyectoExtendido> GetVisibleConTareas();

    Proyecto GetById(int id);
    int Insert(Proyecto proyecto);
    void Update(Proyecto proyecto);
    void UpdateList(List<int> idProyectos);
    void Delete(int id);
  }
}
