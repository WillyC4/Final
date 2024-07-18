namespace Final;

public partial class ConsultaPerfecta : ContentPage
{
	public ConsultaPerfecta()
	{
		InitializeComponent();
	}

    private void btnSiguiente(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ShopAutosView());
    }
}