using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PetCareApp.Models;

[Table("VetVisits")]
public class VetVisit
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int PetId { get; set; }          // The pet that visited the vet

    public DateTime Date { get; set; }      // Date of the vet visit
    public string Notes { get; set; } = string.Empty;  // Notes (e.g., diagnoses, procedures done)
}