using PetCareApp.Models;

namespace PetCareApp.Views;

public partial class PetListPage : ContentPage
{
    public PetListPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPets();
    }

    private void LoadPets()
    {
        var pets = App.PetRepo.GetAllPets();
        petListView.ItemsSource = pets;
    }

    private async void OnAddPetClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddPetPage));
    }

    private async void OnPetSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Pet selectedPet)
        {
            petListView.SelectedItem = null;
            await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}?petId={selectedPet.Id}");
        }
    }
    private async void OnEditPetClicked(object sender, EventArgs e)
    {
        if (sender is MenuItem mi && mi.CommandParameter is Pet pet)
        {
            await Shell.Current.GoToAsync($"{nameof(AddPetPage)}?petId={pet.Id}");
        }
    }

    private async void OnDeletePetClicked(object sender, EventArgs e)
    {
        if (sender is MenuItem mi && mi.CommandParameter is Pet pet)
        {
            bool ok = await DisplayAlert("Sil", $"{pet.Name} silinsin mi?", "Evet", "Vazgeç");
            if (!ok) return;

            var logs = App.LogRepo.GetLogsByPet(pet.Id);
            foreach (var l in logs) App.LogRepo.DeleteLog(l);

            var visits = App.VetRepo.GetVisitsByPet(pet.Id);
            foreach (var v in visits) App.VetRepo.DeleteVisit(v);

            var tasks = App.TaskRepo.GetTasksByPet(pet.Id);
            foreach (var t in tasks) App.TaskRepo.DeleteTask(t);

            App.PetRepo.DeletePet(pet);

            LoadPets(); 
        }
    }

}