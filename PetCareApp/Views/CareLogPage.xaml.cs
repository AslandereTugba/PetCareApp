using PetCareApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace PetCareApp.Views;
[QueryProperty(nameof(PetId), "petId")]
public partial class CareLogPage : ContentPage
{
    public int PetId { get; set; }

    public CareLogPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadCareLogs();
    }

    private void LoadCareLogs()
    {
        var pet = App.PetRepo.GetPet(PetId);
        if (pet != null)
        {
            headerLabel.Text = $"{pet.Name} - Care Log";
        }

        var logs = App.LogRepo.GetLogsByPet(PetId);
        logs = logs.OrderByDescending(log => log.Date).ToList();
        var tasks = App.TaskRepo.GetTasksByPet(PetId);
        var taskNameById = tasks.ToDictionary(t => t.Id, t => t.Name);

        var displayList = new List<KeyValuePair<string, string>>();
        foreach (var log in logs)
        {
            string taskName = taskNameById.ContainsKey(log.TaskId) ? taskNameById[log.TaskId] : $"Task {log.TaskId}";
            string dateStr = log.Date.ToString("g"); 
            displayList.Add(new KeyValuePair<string, string>(taskName, dateStr));
        }

        logListView.ItemsSource = displayList;
    }
}
