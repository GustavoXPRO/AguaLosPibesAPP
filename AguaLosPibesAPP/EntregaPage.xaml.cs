namespace AguaLosPibesAPP;

[QueryProperty(nameof(ProductoSeleccionado), "producto")]
public partial class EntregaPage : ContentPage
{
    private string _productoSeleccionado = string.Empty;

    public string ProductoSeleccionado
    {
        get => _productoSeleccionado;
        set
        {
            _productoSeleccionado = Uri.UnescapeDataString(value ?? string.Empty);
            OnPropertyChanged();
            ActualizarEtiqueta();
        }
    }

    public EntregaPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ActualizarEtiqueta();
    }

    private void ActualizarEtiqueta()
    {
        var lbl = this.FindByName<Label>("lblProducto");
        if (lbl != null)
        {
            lbl.Text = _productoSeleccionado;
        }
    }

    private async void OnEnvioDomicilioClicked(object sender, EventArgs e)
    {
        var txtDir = this.FindByName<Entry>("txtDireccion");
        string direccion = txtDir?.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(direccion))
        {
            await this.DisplayAlertAsync("Atención", "Por favor ingresa tu dirección para el envío a domicilio.", "OK");
            return;
        }

        string tipoEntrega = $"Envío a Domicilio ({direccion.Trim()})";
        await NavegarAConfirmacion(tipoEntrega);
    }

    private async void OnRetiroLocalClicked(object sender, EventArgs e)
    {
        string tipoEntrega = "Retiro en Local (Limpieza y sanitización de envase incluida)";
        await NavegarAConfirmacion(tipoEntrega);
    }

    private async Task NavegarAConfirmacion(string tipoEntrega)
    {
        string productoEscapado = Uri.EscapeDataString(_productoSeleccionado ?? "");
        string entregaEscapada = Uri.EscapeDataString(tipoEntrega);

        await Shell.Current.GoToAsync($"{nameof(ConfirmacionPage)}?producto={productoEscapado}&entrega={entregaEscapada}");
    }
}