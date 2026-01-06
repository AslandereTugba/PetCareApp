using PetCareApp.Models;

namespace PetCareApp.Views;

[QueryProperty(nameof(PetId), "petId")]
public partial class PetDetailPage : ContentPage
{
    public int PetId { get; set; }

    public PetDetailPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPetAndTasks();
    }

    private void LoadPetAndTasks()
    {
        var pet = App.PetRepo.GetPet(PetId);

        if (pet != null)
        {
            petNameLabel.Text = pet.Name;

            var typeText = string.IsNullOrWhiteSpace(pet.Type) ? "" : pet.Type;
            var birthText = $"Born {pet.BirthDate:d}";
            petDetailsLabel.Text = string.IsNullOrEmpty(typeText) ? birthText : $"{typeText}, {birthText}";
        }
        else
        {
            petNameLabel.Text = "Pet";
            petDetailsLabel.Text = "";
        }

        var tasks = App.TaskRepo.GetTasksByPet(PetId)
            .OrderBy(t => t.NextDue)
            .ToList();

        taskListView.ItemsSource = tasks;
    }

    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(AddTaskPage)}?petId={PetId}");
    }

    private void OnTaskDoneClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is CareTask task)
        {
            App.TaskService.CompleteTask(task);
            LoadPetAndTasks();
        }
    }
    private async void OnVetVisitsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(VetVisitPage)}?petId={PetId}");
    }

    private async void OnViewLogsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(CareLogPage)}?petId={PetId}");
    }
}