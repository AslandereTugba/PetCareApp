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
        // Retrieve all pets from the repository and bind to ListView
        var pets = App.PetRepo.GetAllPets();
        petListView.ItemsSource = pets;
    }

    private async void OnAddPetClicked(object sender, EventArgs e)
    {
        // Navigate to the AddPetPage for adding a new pet
        await Shell.Current.GoToAsync(nameof(AddPetPage));
    }

    private async void OnPetSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Pet selectedPet)
        {
            // (Navigation to pet detail will be handled in the next commit when PetDetailPage is implemented)
            // Deselect the item
            petListView.SelectedItem = null;
            // Future navigation to PetDetailPage will go here
        }
    }
}