using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PetCareApp.Models;

namespace PetCareApp.Repositories;

public class VetVisitRepository
{
    private SQLiteConnection db;

    public VetVisitRepository()
    {
        db = App.Db;
    }

    public List<VetVisit> GetVisitsByPet(int petId)
    {
        return db.Table<VetVisit>()
                 .Where(v => v.PetId == petId)
                 .ToList();
    }

    public int AddVisit(VetVisit visit)
    {
        return db.Insert(visit);
    }

    public int DeleteVisit(VetVisit visit)
    {
        return db.Delete(visit);
    }
}
