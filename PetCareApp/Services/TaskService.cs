using PetCareApp.Models;

namespace PetCareApp.Services;

public class TaskService
{
    public void CompleteTask(CareTask task, DateTime? completedDate = null)
    {
        DateTime doneDate = completedDate ?? DateTime.Now;
        task.LastDone = doneDate;

        try
        {
            task.NextDue = doneDate.AddDays(task.FrequencyDays);
        }
        catch
        {
            task.NextDue = doneDate; 
        }
        App.TaskRepo.SaveTask(task);
        CareLog log = new CareLog
        {
            PetId = task.PetId,
            TaskId = task.Id,
            Date = doneDate,
            Notes = ""  
        };
        App.LogRepo.AddLog(log);
    }

    public List<CareTask> GetOverdueTasks()
    {
        var allTasks = App.TaskRepo.GetAllTasks();
        DateTime today = DateTime.Today;
        return allTasks.Where(t => t.NextDue.Date < today).ToList();
    }

    public List<CareTask> GetTodayTasks()
    {
        var allTasks = App.TaskRepo.GetAllTasks();
        DateTime today = DateTime.Today;
        return allTasks.Where(t => t.NextDue.Date == today).ToList();
    }
}
