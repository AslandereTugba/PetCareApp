using SQLite;
using SQLitePCL;
using System.IO;
using Microsoft.Maui.Storage;

namespace PetCareApp;

public partial class App : Application
{
    public static SQLiteConnection Db { get; private set; }

    public App()
    {
        InitializeComponent();

        // Initialize SQLite for all platforms (needed especially for iOS)
        Batteries_V2.Init();

        // Determine database file path
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "petcare.db3");

        // Create SQLite connection and tables
        Db = new SQLiteConnection(dbPath);
        Db.CreateTable<Models.Pet>();
        Db.CreateTable<Models.CareTask>();
        Db.CreateTable<Models.CareLog>();
        Db.CreateTable<Models.VetVisit>();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
