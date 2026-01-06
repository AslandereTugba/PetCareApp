using PetCareApp.Models;
using UIKit;

namespace PetCareApp.Views;

[QueryProperty(nameof(PetId), "petId")]
public partial class AddTaskPage : ContentPage
{
    public int PetId { get; set; }

    public AddTaskPage()
    {
        InitializeComponent();
        dueDatePicker.Date = DateTime.Today;
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

        var newTask = new CareTask
        {
            PetId = PetId,
            Name = name,
            Description = "",
            FrequencyDays = frequencyDays,
            LastDone = null,
            NextDue = dueDatePicker.Date
        };

        App.TaskRepo.SaveTask(newTask);
        await Shell.Current.GoToAsync("..");
    }
}