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
        return db.Table<CareLog>()
                 .Where(log => log.PetId == petId)
                 .ToList();
    }

    public int AddLog(CareLog log)
    {
        return db.Insert(log);
    }

    public List<CareLog> GetLogsForDay(DateTime day)
    {
        var start = day.Date;
        var end = start.AddDays(1);

        return db.Table<CareLog>()
                 .Where(l => l.Date >= start && l.Date < end)
                 .OrderByDescending(l => l.Date)
                 .ToList();
    }

    public int DeleteLog(CareLog log)
    {
        return db.Delete(log);
    }
}
