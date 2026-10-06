namespace AguaLosPibesAPP;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registramos las rutas de las pantallas para poder navegar hacia ellas
       
        Routing.RegisterRoute(nameof(EntregaPage), typeof(EntregaPage));
        Routing.RegisterRoute(nameof(ConfirmacionPage), typeof(ConfirmacionPage));
    }
}

// Routing.RegisterRoute le asigna una "dirección" o "ruta" a cada pantalla dentro del sistema de navegación Shell.
// Es idéntico a cómo funcionan las URLs de una página web (/EntregaPage).