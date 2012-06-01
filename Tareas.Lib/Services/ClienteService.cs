using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tareas.Lib.Models;
using PetaPoco;

namespace Tareas.Lib.Services
{
  public class ClienteService : IClienteService
  {
    public ClienteService()
    {
      db = new Database("Tareas");
    }

    public List<Cliente> GetAll()
    {
      var sql = Sql.Builder
          .Select("*")
          .From(tabla);

      return db.Query<Cliente>(sql).ToList();
    }

    public List<Cliente> GetAllActive()
    {
      var sql = Sql.Builder
          .Select("*")
          .From(tabla)
          .Where("activo = 1");

      return db.Query<Cliente>(sql).ToList();
    }

    public Cliente GetById(int id)
    {
      var sql = Sql.Builder.Where("id = @0", id);
      return db.SingleOrDefault<Cliente>(sql);
    }

    public int Insert(Cliente cliente)
    {
        return Convert.ToInt32(db.Insert(cliente));
    }

    public void Update(Cliente cliente)
    {
        db.Update(cliente);
    }

    public void Delete(int id)
    {
        db.Delete(tabla, "id", null, id);
    }

    private Database db;
    private const string tabla = "cliente";
  }

  public interface IClienteService
  {
    List<Cliente> GetAll();
    List<Cliente> GetAllActive();

    Cliente GetById(int id);
    int Insert(Cliente cliente);
    void Update(Cliente cliente);
    void Delete(int id);
    
  }
}
