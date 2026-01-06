using PetCareApp.Models;

namespace PetCareApp.Views;

public partial class AddPetPage : ContentPage
{
    public AddPetPage()
    {
        InitializeComponent();
        birthDatePicker.Date = DateTime.Today;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        string name = nameEntry.Text?.Trim() ?? "";
        string type = typeEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlert("Validation", "Please enter a name for your pet.", "OK");
            return;
        }

        var newPet = new Pet
        {
            Name = name,
            Type = type,
            BirthDate = birthDatePicker.Date
        };

        App.PetRepo.SavePet(newPet);
        await Shell.Current.GoToAsync("..");
    }
}