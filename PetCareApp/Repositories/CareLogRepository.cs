using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PetCareApp.Models;

namespace PetCareApp.Repositories;

public class CareLogRepository
{
    private SQLiteConnection db;

    public CareLogRepository()
    {
        db = App.Db;
    }

    public List<CareLog> GetLogsByPet(int petId)
    {
        // Retrieve all care logs for the given pet
        return db.Table<CareLog>()
                 .Where(log => log.PetId == petId)
                 .ToList();
    }

    public int AddLog(CareLog log)
    {
        return db.Insert(log);
    }

    public int DeleteLog(CareLog log)
    {
        return db.Delete(log);
    }
}
