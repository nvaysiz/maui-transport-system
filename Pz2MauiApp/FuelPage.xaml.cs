using Pz2MauiApp.ViewModel;

namespace Pz2MauiApp;

public partial class FuelPage : ContentPage
{
    public FuelPage()
    {
        InitializeComponent();
        BindingContext = new FuelViewModel();
    }

    private async void IconButton_PointerEntered(object sender, PointerEventArgs e)
    {
        if (sender is ImageButton btn)
            await btn.ScaleTo(1.12, 100);
    }

    private async void IconButton_PointerExited(object sender, PointerEventArgs e)
    {
        if (sender is ImageButton btn)
            await btn.ScaleTo(1.0, 100);
    }

    private async void IconButton_Pressed(object sender, EventArgs e)
    {
        if (sender is ImageButton btn)
            await btn.ScaleTo(0.9, 70);
    }

    private async void IconButton_Released(object sender, EventArgs e)
    {
        if (sender is ImageButton btn)
            await btn.ScaleTo(1.08, 70);
    }
}