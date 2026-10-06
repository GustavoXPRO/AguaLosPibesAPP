namespace AguaLosPibesAPP;

// Capturamos los dos parámetros enviados desde la pantalla anterior
[QueryProperty(nameof(Producto), "producto")]
[QueryProperty(nameof(Entrega), "entrega")]
public partial class ConfirmacionPage : ContentPage
{
    private string _producto = string.Empty;
    private string _entrega = string.Empty;

    public string Producto
    {
        get => _producto;
        set
        {
            _producto = Uri.UnescapeDataString(value ?? string.Empty);
            OnPropertyChanged();
            ActualizarResumen();
        }
    }

    public string Entrega
    {
        get => _entrega;
        set
        {
            _entrega = Uri.UnescapeDataString(value ?? string.Empty);
            OnPropertyChanged();
            ActualizarResumen();
        }
    }

    public ConfirmacionPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ActualizarResumen();
    }

    private void ActualizarResumen()
    {
        var lblProd = this.FindByName<Label>("lblProductoConfirmado");
        if (lblProd != null)
        {
            lblProd.Text = _producto;
        }

        var lblEnt = this.FindByName<Label>("lblEntregaConfirmada");
        if (lblEnt != null)
        {
            lblEnt.Text = _entrega;
        }
    }

    private async void OnConfirmarPedidoClicked(object sender, EventArgs e)
    {
        // Mostramos un mensaje de éxito simulando que el pedido fue registrado
        await this.DisplayAlertAsync("¡Pedido Recibido!", "Tu pedido ha sido registrado con éxito. Muchas gracias.", "Aceptar");

        // Regresamos a la pantalla principal
        await Shell.Current.GoToAsync("//InicioPage");
    }

    private async void OnVolverInicioClicked(object sender, EventArgs e)
    {
        // Regresa a la pantalla de inicio sin procesar nada
        await Shell.Current.GoToAsync("//InicioPage");
    }
}