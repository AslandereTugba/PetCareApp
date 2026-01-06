using SQLite;
using SQLitePCL;
using System.IO;
using Microsoft.Maui.Storage;

namespace PetCareApp;

public partial class App : Application
{
    public static SQLite.SQLiteConnection Db { get; private set; } = null!;
    public static Repositories.PetRepository PetRepo { get; private set; } = null!;
    public static Repositories.CareTaskRepository TaskRepo { get; private set; } = null!;
    public static Repositories.CareLogRepository LogRepo { get; private set; } = null!;
    public static Repositories.VetVisitRepository VetRepo { get; private set; } = null!;
    public static Services.TaskService TaskService { get; private set; } = null!;
    public App()
    {
        InitializeComponent();

        Batteries_V2.Init();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "petcare.db3");

        Db = new SQLiteConnection(dbPath);
        Db.CreateTable<Models.Pet>();
        Db.CreateTable<Models.CareTask>();
        Db.CreateTable<Models.CareLog>();
        Db.CreateTable<Models.VetVisit>();

        PetRepo = new Repositories.PetRepository();
        TaskRepo = new Repositories.CareTaskRepository();
        LogRepo = new Repositories.CareLogRepository();
        VetRepo = new Repositories.VetVisitRepository();
        TaskService = new Services.TaskService();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
