namespace Pz2MauiApp;

public partial class MainPage : ContentPage
{
    private readonly Color _normalStroke = Color.FromArgb("#8ECFE8");
    private readonly Color _hoverStroke = Color.FromArgb("#5DB7DA");
    private readonly Color _normalBackground = Color.FromArgb("#B7E3F6");
    private readonly Color _hoverBackground = Color.FromArgb("#CDEFFC");

    public MainPage()
    {
        InitializeComponent();
    }

    private async void GoToVehicle(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//VehiclePage");
    }

    private async void GoToFuel(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//FuelPage");
    }

    private async void GoToTrip(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//TripPage");
    }

    private async void Card_PointerEntered(object sender, PointerEventArgs e)
    {
        if (sender is Border card)
        {
            await card.ScaleTo(1.03, 120);
            card.Stroke = _hoverStroke;
            card.BackgroundColor = _hoverBackground;
        }
    }

    private async void Card_PointerExited(object sender, PointerEventArgs e)
    {
        if (sender is Border card)
        {
            await card.ScaleTo(1.0, 120);
            card.Stroke = _normalStroke;
            card.BackgroundColor = _normalBackground;
        }
    }

    private async void Card_PointerPressed(object sender, PointerEventArgs e)
    {
        if (sender is Border card)
        {
            await card.ScaleTo(0.97, 70);
        }
    }

    private async void Card_PointerReleased(object sender, PointerEventArgs e)
    {
        if (sender is Border card)
        {
            await card.ScaleTo(1.03, 70);
        }
    }
}