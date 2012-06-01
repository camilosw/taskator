using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tareas.Lib.Models;
using PetaPoco;

namespace Tareas.Lib.Services
{
  public class LogTrabajoTareasService : ILogTrabajoTareasService
  {
    public LogTrabajoTareasService()
    {
      db = new Database("Tareas");
    }

    public int getTiempoPorTarea(int id)
    {
      var sql = Sql.Builder
        .Select("sum(log_trabajo_tareas.minutos) as TiempoEjecutado")
        .From(tabla)
        .Where("id_tarea = @0", id)
        .GroupBy("log_trabajo_tareas.id_tarea");

      return (int)db.ExecuteScalar<float>(sql); ;
    }

    public int Insert(LogTrabajoTareas logTrabajoTareas)
    {
      return Convert.ToInt32(db.Insert(logTrabajoTareas));
    }

    public void Update(LogTrabajoTareas logTrabajoTareas)
    {
      db.Update(logTrabajoTareas);
    }

    public void LogTrabajo(int idTarea, int idPersona, DateTime fecha)
    {
      // Busca el último log correspondiente a la persona y a la tarea cuya fecha fin
      // esté dentro del último minuto
      var sql = Sql.Builder.Select("id")
        .From(tabla)
        .Where("id_tarea = @0 and id_persona = @1 and fecha_fin > date_sub(now(), interval 1 minute)", 
          idTarea, idPersona)
        .OrderBy("fecha_fin desc")
        .Append("limit 1");

      var logTarea = db.SingleOrDefault<LogTrabajoTareas>(sql);

      // Busca el último log de la persona
      sql = Sql.Builder.Select("id")
        .From(tabla)
        .Where("id_persona = @0", idPersona)
        .OrderBy("fecha_fin desc")
        .Append("limit 1");

      var ultimoLogTarea = db.SingleOrDefault<LogTrabajoTareas>(sql);
            
      // Verifica si el último log de la persona coincide con el último log de la tarea
      if (logTarea != null && ultimoLogTarea != null && logTarea.Id == ultimoLogTarea.Id)
      {
        sql = Sql.Builder.Where("id = @0", logTarea.Id);
        var log = db.SingleOrDefault<LogTrabajoTareas>(sql);
        log.FechaFin = fecha;
        var minutos = log.FechaFin.Subtract(log.FechaInicio);
        log.Minutos = (float)minutos.TotalMinutes;
        db.Update(log);
      }
      else
      {
        var log = new LogTrabajoTareas()
        {
          IdTarea = idTarea,
          IdPersona = idPersona,
          FechaInicio = fecha,
          FechaFin = fecha,
          Minutos = 0
        };

        db.Insert(log);
      }
    }

    private Database db;
    private const string tabla = "log_trabajo_tareas";
  }
  
  public interface ILogTrabajoTareasService
  {
    int getTiempoPorTarea(int id);
    int Insert(LogTrabajoTareas logTrabajoTareas);
    void Update(LogTrabajoTareas logTrabajoTareas);
    void LogTrabajo(int idTarea, int idPersona, DateTime fecha);
  }
}
