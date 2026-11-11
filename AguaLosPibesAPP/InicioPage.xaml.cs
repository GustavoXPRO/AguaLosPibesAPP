namespace AguaLosPibesAPP;

public partial class InicioPage : ContentPage
{
    public InicioPage()
    {
        InitializeComponent();
    }

    private async void OnRecargaClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"EntregaPage?producto=Recarga de Agua (20L) - $5.000");
    }

    private async void OnCompraInicialClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"EntregaPage?producto=Kit Envase + Carga (20L) - $20.000");
    }

    private async void OnDispenserClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"EntregaPage?producto=Solicitar Dispenser Nuevo - $15.000");
    }

    private async void OnPerfilClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PerfilPage));
    }

    private async void OnHistorialClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HistorialPage));
    }

    private async void OnAyudaWhatsAppClicked(object sender, EventArgs e)
    {
        try
        {
            string numeroTelefono = "5493718497758  ";
            string mensaje = "Hola, necesito soporte con la app Agua Los Pibes.";
            string url = $"https://wa.me/{numeroTelefono}?text={Uri.EscapeDataString(mensaje)}";
            await Launcher.OpenAsync(new Uri(url));
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "No se pudo abrir WhatsApp.", "OK");
        }
    }

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        Preferences.Remove("IsLoggedIn");
        Preferences.Remove("LoggedUser");
        await Shell.Current.GoToAsync("//LoginPage");
    }
}