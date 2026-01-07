using PetCareApp.Models;

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
            var groupItems = overdueTasks.Select(t => new TaskDisplayItem(t, showDone: true)).ToList();
            taskGroups.Add(new TaskGroup("Overdue Tasks", groupItems));
        }

        if (todayTasks.Any())
        {
            var groupItems = todayTasks.Select(t => new TaskDisplayItem(t, showDone: true)).ToList();
            taskGroups.Add(new TaskGroup("Today's Tasks", groupItems));
        }

        var logsToday = App.LogRepo.GetLogsForDay(DateTime.Today);
        if (logsToday.Any())
        {
            var allTasks = App.TaskRepo.GetAllTasks();
            var taskNameById = allTasks.ToDictionary(x => x.Id, x => x.Name);

            var completedItems = logsToday
                .OrderByDescending(l => l.Date)
                .Select(l =>
                {
                    var pet = App.PetRepo.GetPet(l.PetId);
                    string petName = pet != null ? pet.Name : $"Pet {l.PetId}";

                    string taskName = !string.IsNullOrWhiteSpace(l.TaskNameSnapshot)
                        ? l.TaskNameSnapshot
                        : (taskNameById.ContainsKey(l.TaskId) ? taskNameById[l.TaskId] : $"Task {l.TaskId}");


                    return TaskDisplayItem.Completed($"{petName}: {taskName} (Done {l.Date:t})");
                })
                .ToList();

            taskGroups.Add(new TaskGroup("Today's Completed Tasks", completedItems));
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
            App.TaskService.CompleteTask(task);
            LoadDashboard();
        }
    }
}

public class TaskDisplayItem
{
    public CareTask? Task { get; }
    public string DisplayText { get; }
    public bool ShowDone { get; }

    public TaskDisplayItem(CareTask task, bool showDone)
    {
        Task = task;
        ShowDone = showDone;

        var pet = App.PetRepo.GetPet(task.PetId);
        string petName = pet != null ? pet.Name : $"Pet {task.PetId}";
        DisplayText = $"{petName}: {task.Name} (Due {task.NextDue:d})";
    }

    private TaskDisplayItem(string text)
    {
        Task = null;
        ShowDone = false;
        DisplayText = text;
    }

    public static TaskDisplayItem Completed(string text) => new TaskDisplayItem(text);
}

public class TaskGroup : List<TaskDisplayItem>
{
    public string Title { get; }

    public TaskGroup(string title, List<TaskDisplayItem> items) : base(items)
    {
        Title = title;
    }
}