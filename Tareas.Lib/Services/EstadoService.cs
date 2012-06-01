using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tareas.Lib.Models;

namespace Tareas.Lib.Services
{
  public class EstadoService : IEstadoService
  {
    public EstadoService()
    {
        db = new Database("Tareas");
    }

    public Estado GetByNombre(string nombre)
    {
      var sql = Sql.Builder.Where("nombre = @0", nombre);
      return db.SingleOrDefault<Estado>(sql);
    }
      
    public List<Estado> GetAll()
    {
        var sql = Sql.Builder.Select("*").From(tabla).OrderBy("orden");
        return db.Query<Estado>(sql).ToList();
    }

    public List<KeyValuePair<string, string>> GetIdValueList()
    {
        var sql = Sql.Builder.Select("id, nombre").From(tabla).OrderBy("orden");
        var results = db.Query<Estado>(sql);

        List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
        foreach (var item in results)
        {
            list.Add(new KeyValuePair<string, string>(item.Id.ToString(), item.Nombre));
        }
        return list;
    }

    private Database db;
    private const string tabla = "estado";
  }

  public interface IEstadoService
  {
    List<KeyValuePair<string, string>> GetIdValueList();
    List<Estado> GetAll();
    Estado GetByNombre(string nombre);
  }
}
