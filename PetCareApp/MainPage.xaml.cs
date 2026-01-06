using PetCareApp.Models;
using System.Collections.ObjectModel;

namespace PetCareApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadDashboard();
    }

    private void LoadDashboard()
    {
        var overdueTasks = App.TaskService.GetOverdueTasks();
        var todayTasks = App.TaskService.GetTodayTasks();

        var taskGroups = new List<TaskGroup>();

        if (overdueTasks.Any())
        {
            var groupItems = overdueTasks.Select(t => new TaskDisplayItem(t)).ToList();
            taskGroups.Add(new TaskGroup("Overdue Tasks", groupItems));
        }
        if (todayTasks.Any())
        {
            var groupItems = todayTasks.Select(t => new TaskDisplayItem(t)).ToList();
            taskGroups.Add(new TaskGroup("Today's Tasks", groupItems));
        }
        if (!taskGroups.Any())
        {
            taskGroups.Add(new TaskGroup("No tasks due today", new List<TaskDisplayItem>()));
        }

        tasksListView.ItemsSource = taskGroups;
    }

    private void OnTaskDoneClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is CareTask task)
        {
            // Complete the task and refresh the dashboard
            App.TaskService.CompleteTask(task);
            LoadDashboard();
        }
    }
}

public class TaskDisplayItem
{
    public CareTask Task { get; }
    public string DisplayText { get; }

    public TaskDisplayItem(CareTask task)
    {
        Task = task;
        // Include pet name and task name in display
        var pet = App.PetRepo.GetPet(task.PetId);
        string petName = pet != null ? pet.Name : $"Pet {task.PetId}";
        DisplayText = $"{petName}: {task.Name} (Due {task.NextDue:d})";
    }
}

public class TaskGroup : List<TaskDisplayItem>
{
    public string Title { get; }

    public TaskGroup(string title, List<TaskDisplayItem> items) : base(items)
    {
        Title = title;
    }
}