using PetCareApp.Models;
using System.Linq;

namespace PetCareApp.Views;

[QueryProperty(nameof(PetId), "petId")]
public partial class VetVisitPage : ContentPage
{
    public int PetId { get; set; }

    private readonly List<(CareTask task, CheckBox checkbox)> taskCheckboxes = new();

    public VetVisitPage()
    {
        InitializeComponent();
        visitDatePicker.Date = DateTime.Today;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var pet = App.PetRepo.GetPet(PetId);
        headerLabel.Text = pet != null ? $"{pet.Name} - Vet Visits" : "Vet Visits";

        LoadVetVisits();
        LoadTaskChecklist();
    }

    private void LoadVetVisits()
    {
        var visits = App.VetRepo.GetVisitsByPet(PetId)
                               .OrderByDescending(v => v.Date)
                               .ToList();

        visitListView.ItemsSource = visits;
    }

    private void LoadTaskChecklist()
    {
        taskCheckboxes.Clear();
        tasksChecklist.Children.Clear();

        var tasks = App.TaskRepo.GetTasksByPet(PetId);
        foreach (var task in tasks)
        {
            var cb = new CheckBox();
            var lbl = new Label
            {
                Text = task.Name,
                VerticalTextAlignment = TextAlignment.Center
            };

            var row = new HorizontalStackLayout { Spacing = 10 };
            row.Children.Add(cb);
            row.Children.Add(lbl);

            tasksChecklist.Children.Add(row);
            taskCheckboxes.Add((task, cb));
        }
    }

    private async void OnSaveVisitClicked(object sender, EventArgs e)
    {
        DateTime visitDate = visitDatePicker.Date;
        string notes = notesEntry.Text?.Trim() ?? string.Empty;

        var completedTasks = taskCheckboxes
            .Where(x => x.checkbox.IsChecked)
            .Select(x => x.task)
            .ToList();

        App.VetService.AddVetVisit(PetId, visitDate, notes, completedTasks);

        notesEntry.Text = string.Empty;
        foreach (var (_, cb) in taskCheckboxes) cb.IsChecked = false;

        LoadVetVisits();

        await DisplayAlert("OK", "Vet visit saved.", "OK");
        await Shell.Current.GoToAsync("..");
    }
}