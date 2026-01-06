using PetCareApp.Models;

namespace PetCareApp.Views;

[QueryProperty(nameof(PetId), "petId")]
[QueryProperty(nameof(TaskId), "taskId")]
public partial class AddTaskPage : ContentPage
{
    public int PetId { get; set; }
    public int TaskId { get; set; }

    public AddTaskPage()
    {
        InitializeComponent();
        dueDatePicker.Date = DateTime.Today;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (TaskId > 0)
        {
            Title = "Edit Task";
            var task = App.TaskRepo.GetTask(TaskId);
            if (task != null)
            {
                nameEntry.Text = task.Name;
                frequencyEntry.Text = task.FrequencyDays.ToString();
                dueDatePicker.Date = task.NextDue;
            }
        }
        else
        {
            Title = "Add Task";
            dueDatePicker.Date = DateTime.Today;
        }
    }
    private async void OnSaveTaskClicked(object sender, EventArgs e)
    {
        string name = nameEntry.Text?.Trim() ?? "";
        string freqText = frequencyEntry.Text?.Trim() ?? "0";

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlert("Validation", "Please enter a task name.", "OK");
            return;
        }

        if (!int.TryParse(freqText, out int frequencyDays) || frequencyDays < 0)
        {
            await DisplayAlert("Validation", "Please enter a valid non-negative number for frequency.", "OK");
            return;
        }

        CareTask task = (TaskId > 0) ? (App.TaskRepo.GetTask(TaskId) ?? new CareTask()) : new CareTask();

        task.PetId = PetId;
        task.Name = name;
        task.FrequencyDays = frequencyDays;
        task.NextDue = dueDatePicker.Date;

        if (TaskId > 0) task.Id = TaskId;

        App.TaskRepo.SaveTask(task);
        await Shell.Current.GoToAsync("..");
    }
}