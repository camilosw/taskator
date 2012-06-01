using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tareas.Lib.Models;
using System.Text.RegularExpressions;

namespace Tareas.Lib.Services
{
  public class TareaService : ITareaService
  {
    public TareaService()
    {
      db = new Database("Tareas");
    }

    public Tarea GetById(int id)
    {
      var sql = Sql.Builder.Where("id = @0", id);
      return db.SingleOrDefault<Tarea>(sql);
    }

    public int Insert(Tarea tarea)
    {
      tarea.Orden = GetMaxTareaOrden() + 1;
      tarea.Creada = DateTime.Now;
      tarea.Actualizada = tarea.Creada;
      return Convert.ToInt32(db.Insert(tarea));
    }

    public void Update(Tarea tarea)
    {
      var estadoService = new EstadoService();
      var estadoPendiente = estadoService.GetByNombre("Pendiente");
      var estadoEnProceso = estadoService.GetByNombre("En proceso");

      // Si la tarea cambia al estado en proceso y está asignada a un usuario, busca las demás tareas
      // de ese usuario que tengan ese mismo estado y las cambia a estado pendiente
      if (tarea.IdEstado == estadoEnProceso.Id && tarea.IdAsignado.HasValue)          
      {
        // Consulta el estado de la tarea en base de datos, para verificar si cambió el estado o 
        // cambió la asignación de persona
        var tareaDB = GetById(tarea.Id);
        if (tareaDB.IdEstado != estadoEnProceso.Id || tareaDB.IdAsignado != tarea.IdAsignado.Value)
        {
          // Busca las tareas del usuario que estén en proceso
          var sql = Sql.Builder.Where("id_asignado = @0 and id_estado = @1", tarea.IdAsignado.Value, estadoEnProceso.Id);
          var tareas = db.Query<Tarea>(sql).ToList();

          // Las demás tareas que estén en estado en proceso, las cambia al estado pendiente
          foreach (var item in tareas)
          {
            if (item.Id != tarea.Id)
            {
              item.IdEstado = estadoPendiente.Id;
              item.Actualizada = DateTime.Now;
              db.Update(item);
            }
          }

          // Empieza a registrar el tiempo de trabajo de la tarea
          new LogTrabajoTareasService().LogTrabajo(tarea.Id, tarea.IdAsignado.Value, DateTime.Now);
        }
      }

      tarea.Actualizada = DateTime.Now;
      db.Update(tarea);
    }

    public void UpdateList(List<IdProyectosTareas> listadoIdProyectosTareas)
    {
      foreach (var idProyectoTareas in listadoIdProyectosTareas)
      {
        int i = 0;
        if (idProyectoTareas.IdTareas == null)
        {
          continue;
        }
        foreach (var idTarea in idProyectoTareas.IdTareas)
        {
          var tarea = GetById(idTarea);
          tarea.Orden = i;
          tarea.IdProyecto = idProyectoTareas.IdProyecto;
          tarea.Actualizada = DateTime.Now;
          Update(tarea);
          i++;
        }
      }
    }

    private int GetMaxTareaOrden()
    {
      var sql = Sql.Builder.Select("min(orden)").From(tabla);
      int? lowestSortValue = db.Single<int?>(sql);
      return (lowestSortValue ?? 0);
    }

    /// <summary>
    /// Convierte minutos a un string con el formato 5d 4h 15m
    /// </summary>
    static public string TimeToString(int time)
    {
      string result = "";
      int days = time / 24 / 60;
      if (days > 0) result += days.ToString() + "d ";
      int hours = time / 60 % 24;
      if (hours > 0) result += hours.ToString() + "h ";
      int minutes = time % 60;
      if (minutes > 0) result += minutes.ToString() + "m";
      return result;
    }

    static public int? StringToTime(string text)
    {
      if (string.IsNullOrEmpty(text)) return 0;

      int? result = 0;
      foreach (Match m in Regex.Matches(text, @"(?<numero>\d*)(?<letra>\w?)\s*"))
      {
        if (m.Groups["numero"].Success) {
          int numero;
          if(!Int32.TryParse(m.Groups["numero"].Value, out numero)) continue;
          if (m.Groups["letra"].Success)
          {
            switch (m.Groups["letra"].Value)
            {
              case "d":
                result += numero * 24 * 60;
                break;
              case "h":
                result += numero * 60;
                break;
              case "m":
              default:
                result += numero;
                break;
            }
          }
          else
          {
            result += numero;
          }
        }
      }
      return result == 0 ? null : result;
    }

    private Database db;
    private const string tabla = "tarea";
  }

  public interface ITareaService
  {
    Tarea GetById(int id);
    int Insert(Tarea tarea);
    void Update(Tarea tarea);
    void UpdateList(List<IdProyectosTareas> listadoIdProyectosTareas);
  }
}
