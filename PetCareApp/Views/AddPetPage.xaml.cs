using PetCareApp.Models;

namespace PetCareApp.Views;

[QueryProperty(nameof(PetId), "petId")]
public partial class AddPetPage : ContentPage
{
    public int PetId { get; set; }

    public AddPetPage()
    {
        InitializeComponent();
        birthDatePicker.Date = DateTime.Today;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (PetId > 0)
        {
            Title = "Edit Pet";
            var pet = App.PetRepo.GetPet(PetId);
            if (pet != null)
            {
                nameEntry.Text = pet.Name;
                typeEntry.Text = pet.Type;
                birthDatePicker.Date = pet.BirthDate;
            }
        }
        else
        {
            Title = "Add Pet";
        }
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

        Pet pet = (PetId > 0) ? (App.PetRepo.GetPet(PetId) ?? new Pet()) : new Pet();

        pet.Name = name;
        pet.Type = type;
        pet.BirthDate = birthDatePicker.Date;

        if (PetId > 0) pet.Id = PetId;

        App.PetRepo.SavePet(pet);
        await Shell.Current.GoToAsync("..");
    }
}