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
}