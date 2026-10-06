namespace AguaLosPibesAPP;

public partial class InicioPage : ContentPage
{
    public InicioPage()
    {
        InitializeComponent();
    }

    private async void OnRecargaClicked(object sender, EventArgs e)
    {
        string producto = "Recarga de Agua (20L) - $5.000";
        await Shell.Current.GoToAsync($"{nameof(EntregaPage)}?producto={Uri.EscapeDataString(producto)}");
    }

    private async void OnCompraInicialClicked(object sender, EventArgs e)
    {
        string producto = "Kit Envase y Carga (20L) - $20.000";
        await Shell.Current.GoToAsync($"{nameof(EntregaPage)}?producto={Uri.EscapeDataString(producto)}");
    }

    private async void OnDispenserClicked(object sender, EventArgs e)
    {
        string producto = "Dispenser Nuevo - $15.000";
        await Shell.Current.GoToAsync($"{nameof(EntregaPage)}?producto={Uri.EscapeDataString(producto)}");
    }
}