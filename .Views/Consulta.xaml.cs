using Final.Models;
using Final.Repositorio;
using Final.Views;

namespace Final;

public partial class Consulta : ContentPage
{
    public Consulta()
	{
		InitializeComponent();
    }
    private void btnSiguiente(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ConsultaPerfecta());
    }
}