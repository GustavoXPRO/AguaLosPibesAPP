using System.Text.Json;

namespace AguaLosPibesAPP;

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
        // 1. Guardamos el pedido en el historial local automáticamente
        try
        {
            var nuevoPedido = new PedidoModel
            {
                Producto = string.IsNullOrEmpty(_producto) ? "Pedido de Agua Los Pibes" : _producto,
                Direccion = string.IsNullOrEmpty(_entrega) ? "Formosa Capital" : _entrega,
                Fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                Estado = "Confirmado"
            };

            string jsonActual = Preferences.Get("HistorialPedidos", string.Empty);
            List<PedidoModel> listaPedidos;

            if (string.IsNullOrEmpty(jsonActual))
            {
                listaPedidos = new List<PedidoModel>();
            }
            else
            {
                listaPedidos = JsonSerializer.Deserialize<List<PedidoModel>>(jsonActual) ?? new List<PedidoModel>();
            }

            listaPedidos.Insert(0, nuevoPedido);
            Preferences.Set("HistorialPedidos", JsonSerializer.Serialize(listaPedidos));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Atención", $"No se pudo registrar en el historial: {ex.Message}", "OK");
        }

        // 2. Mensaje de éxito que ya tenías
        await this.DisplayAlertAsync("¡Pedido Recibido!", "Tu pedido ha sido registrado con éxito. Muchas gracias.", "Aceptar");

        // 3. Regresamos a la pantalla principal
        await Shell.Current.GoToAsync("//InicioPage");
    }

    private async void OnVolverInicioClicked(object sender, EventArgs e)
    {
        // Regresa a la pantalla de inicio sin procesar nada
        await Shell.Current.GoToAsync("//InicioPage");
    }
}