using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PetCareApp.Models;

[Table("CareTasks")]
public class CareTask
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed] 
    public int PetId { get; set; }         

    public string Name { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty; 
    public int FrequencyDays { get; set; }   
    public DateTime? LastDone { get; set; }   
    public DateTime NextDue { get; set; }    
}
