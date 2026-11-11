namespace AguaLosPibesAPP;

public partial class PerfilPage : ContentPage
{
    private string _fotoRuta = string.Empty;

    public PerfilPage()
    {
        InitializeComponent();
        CargarDatosGuardados();
    }

    private void CargarDatosGuardados()
    {
        // Cargamos los datos guardados previamente en el dispositivo
        NombreEntry.Text = Preferences.Get("Perfil_Nombre", string.Empty);
        TelefonoEntry.Text = Preferences.Get("Perfil_Telefono", string.Empty);
        DireccionEntry.Text = Preferences.Get("Perfil_Direccion", string.Empty);

        _fotoRuta = Preferences.Get("Perfil_Foto", string.Empty);
        if (!string.IsNullOrEmpty(_fotoRuta) && File.Exists(_fotoRuta))
        {
            FotoPerfilImage.Source = ImageSource.FromFile(_fotoRuta);
        }
    }

    private async void OnTomarFotoClicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    // Guardamos la foto en el directorio local de la app
                    string localFilePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);

                    _fotoRuta = localFilePath;
                    FotoPerfilImage.Source = ImageSource.FromFile(_fotoRuta);
                }
            }
            else
            {
                await DisplayAlert("Error", "La cámara no está disponible en este dispositivo.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo tomar la foto: {ex.Message}", "OK");
        }
    }

    private async void OnElegirGaleriaClicked(object sender, EventArgs e)
    {
        try
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo != null)
            {
                string localFilePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);
                await sourceStream.CopyToAsync(localFileStream);

                _fotoRuta = localFilePath;
                FotoPerfilImage.Source = ImageSource.FromFile(_fotoRuta);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo seleccionar la imagen: {ex.Message}", "OK");
        }
    }

    private async void OnGuardarPerfilClicked(object sender, EventArgs e)
    {
        // Guardamos los datos de manera local con Preferences
        Preferences.Set("Perfil_Nombre", NombreEntry.Text ?? string.Empty);
        Preferences.Set("Perfil_Telefono", TelefonoEntry.Text ?? string.Empty);
        Preferences.Set("Perfil_Direccion", DireccionEntry.Text ?? string.Empty);
        Preferences.Set("Perfil_Foto", _fotoRuta);

        await DisplayAlert("¡Éxito!", "Tus datos de perfil han sido guardados correctamente.", "OK");
    }
}