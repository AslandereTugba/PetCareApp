using PetCareApp.Models;
using System;
using System.Collections.Generic;

namespace PetCareApp.Services;

public class VetVisitService
{
    public void AddVetVisit(int petId, DateTime visitDate, string notes, List<CareTask> tasksCompleted)
    {
        VetVisit visit = new VetVisit
        {
            PetId = petId,
            Date = visitDate,
            Notes = notes
        };
        App.VetRepo.AddVisit(visit);

        foreach (var task in tasksCompleted)
        {
            App.TaskService.CompleteTask(task, visitDate);
        }
    }
}
