using PetCareApp.Models;

namespace PetCareApp.Views;

public partial class AddPetPage : ContentPage
{
    public AddPetPage()
    {
        InitializeComponent();
        // Set default date to today for the birth date picker
        birthDatePicker.Date = DateTime.Today;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        string name = nameEntry.Text?.Trim() ?? string.Empty;
        string type = typeEntry.Text?.Trim() ?? string.Empty;
        DateTime birthDate = birthDatePicker.Date;

        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Validation", "Please enter a name for your pet.", "OK");
            return;
        }

        // Create and save new Pet
        Pet newPet = new Pet
        {
            Name = name,
            Type = type,
            BirthDate = birthDate
        };
        App.PetRepo.SavePet(newPet);

        // Go back to the pet list
        await Shell.Current.GoToAsync("..");
    }
}