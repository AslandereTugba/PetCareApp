using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace PetCareApp.Models;

[Table("Pets")]
public class Pet
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;  
    public DateTime BirthDate { get; set; }        


}

