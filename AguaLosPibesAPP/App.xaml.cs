namespace AguaLosPibesAPP;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Registro de rutas de navegación
        Routing.RegisterRoute(nameof(ConfirmacionPage), typeof(ConfirmacionPage));
        Routing.RegisterRoute(nameof(EntregaPage), typeof(EntregaPage));
        Routing.RegisterRoute(nameof(HistorialPage), typeof(HistorialPage));
        Routing.RegisterRoute(nameof(PerfilPage), typeof(PerfilPage));

        MainPage = new AppShell();
    }

    protected override async void OnStart()
    {
        base.OnStart();

        bool isLoggedIn = Preferences.Get("IsLoggedIn", false);

        if (isLoggedIn)
        {
            await Shell.Current.GoToAsync("//InicioPage");
        }
    }
}