using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PetCareApp.Models;

namespace PetCareApp.Repositories;

public class PetRepository
{
    private SQLiteConnection db;

    public PetRepository()
    {
        db = App.Db; 
    }

    public List<Pet> GetAllPets()
    {
        return db.Table<Pet>().ToList();
    }

    public Pet? GetPet(int id)
    {
        return db.Find<Pet>(id);
    }

    public int SavePet(Pet pet)
    {
        if (pet.Id != 0)
        {
            return db.Update(pet);
        }
        else
        {
            return db.Insert(pet);
        }
    }

    public int DeletePet(Pet pet)
    {
        return db.Delete(pet);
    }
}
