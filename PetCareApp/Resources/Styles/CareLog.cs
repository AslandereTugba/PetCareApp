using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PetCareApp.Models;

[Table("CareLogs")]
public class CareLog
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int PetId { get; set; }           

    [Indexed]
    public int TaskId { get; set; }         

    public DateTime Date { get; set; }      
    public string Notes { get; set; } = string.Empty; 
}