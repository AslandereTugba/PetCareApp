using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PetCareApp.Models;

namespace PetCareApp.Repositories;

public class CareTaskRepository
{
    private SQLiteConnection db;

    public CareTaskRepository()
    {
        db = App.Db;
    }

    public CareTask? GetTask(int id)
    {
        return db.Find<CareTask>(id);
    }

    public List<CareTask> GetTasksByPet(int petId)
    {
        // Fetch all tasks for a given pet
        return db.Table<CareTask>()
                 .Where(t => t.PetId == petId)
                 .ToList();
    }

    public List<CareTask> GetAllTasks()
    {
        return db.Table<CareTask>().ToList();
    }

    public int SaveTask(CareTask task)
    {
        if (task.Id != 0)
        {
            return db.Update(task);
        }
        else
        {
            return db.Insert(task);
        }
    }

    public int DeleteTask(CareTask task)
    {
        return db.Delete(task);
    }
}
