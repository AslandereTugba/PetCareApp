using PetCareApp.Views;

namespace PetCareApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddPetPage), typeof(AddPetPage));
    }
}
