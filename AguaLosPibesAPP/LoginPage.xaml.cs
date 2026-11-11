using AguaLosPibesAPP.Services;

namespace AguaLosPibesAPP;

public partial class LoginPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public LoginPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string user = UsernameEntry.Text;
        string pass = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
        {
            await DisplayAlertAsync("Error", "Por favor completa todos los campos.", "OK");
            return;
        }

        bool isValid = await _databaseService.LoginAsync(user, pass);

        if (isValid)
        {
            // Guardamos la sesión activa
            Preferences.Set("IsLoggedIn", true);
            Preferences.Set("LoggedUser", user);

            await Shell.Current.GoToAsync("//InicioPage");
        }
        else
        {
            await DisplayAlertAsync("Acceso Denegado", "Usuario o contraseña incorrectos.", "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string user = UsernameEntry.Text;
        string pass = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
        {
            await DisplayAlertAsync("Error", "Ingresa un usuario y contraseña para registrarte.", "OK");
            return;
        }

        int result = await _databaseService.RegisterUserAsync(user, pass);

        if (result == -1)
        {
            await DisplayAlertAsync("Atención", "Ese usuario ya está registrado. Prueba iniciar sesión.", "OK");
        }
        else if (result > 0)
        {
            await DisplayAlertAsync("¡Éxito!", "Usuario registrado correctamente. Ya puedes iniciar sesión.", "OK");
            PasswordEntry.Text = string.Empty;
        }
        else
        {
            await DisplayAlertAsync("Error", "No se pudo registrar el usuario.", "OK");
        }
    }
}