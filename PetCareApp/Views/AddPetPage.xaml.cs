using PetCareApp.Models;

namespace PetCareApp.Views;

public partial class AddPetPage : ContentPage
{
    public AddPetPage()
    {
        InitializeComponent();
        birthDatePicker.Date = DateTime.Today;
        birthDatePicker.MaximumDate = DateTime.Today; // ? ekle
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

        Pet newPet = new Pet
        {
            Name = name,
            Type = type,
            BirthDate = birthDate
        };

        App.PetRepo.SavePet(newPet);

        await Shell.Current.GoToAsync("..");
    }
}