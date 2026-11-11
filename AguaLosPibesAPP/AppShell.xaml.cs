namespace AguaLosPibesAPP;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    private async void OnAyudaWhatsAppClicked(object sender, EventArgs e)
    {
        try
        {
            string numeroTelefono = "5493718497758";
            string mensaje = "Hola, necesito soporte con la app Agua Los Pibes.";
            string url = $"https://wa.me/{numeroTelefono}?text={Uri.EscapeDataString(mensaje)}";
            await Launcher.OpenAsync(new Uri(url));
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "No se pudo abrir WhatsApp en este dispositivo.", "OK");
        }
    }

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        Preferences.Remove("IsLoggedIn");
        Preferences.Remove("LoggedUser");
        await Shell.Current.GoToAsync("//LoginPage");
    }
}