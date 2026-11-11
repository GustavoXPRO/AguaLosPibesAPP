using System.Collections.ObjectModel;
using System.Text.Json;

namespace AguaLosPibesAPP;

public partial class HistorialPage : ContentPage
{
    public ObservableCollection<PedidoModel> Pedidos { get; set; } = new();

    public HistorialPage()
    {
        InitializeComponent();
        HistorialCollectionView.ItemsSource = Pedidos;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarHistorial();
    }

    private void CargarHistorial()
    {
        Pedidos.Clear();
        string json = Preferences.Get("HistorialPedidos", string.Empty);

        if (!string.IsNullOrEmpty(json))
        {
            var lista = JsonSerializer.Deserialize<List<PedidoModel>>(json);
            if (lista != null)
            {
                foreach (var item in lista)
                {
                    Pedidos.Add(item);
                }
            }
        }

        // Controlamos si mostramos la lista o el mensaje de vacío
        SinHistorialLabel.IsVisible = Pedidos.Count == 0;
        HistorialCollectionView.IsVisible = Pedidos.Count > 0;
    }
}

// Modelo de datos para cada pedido
public class PedidoModel
{
    public string Producto { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;
    public string Estado { get; set; } = "Confirmado";
}