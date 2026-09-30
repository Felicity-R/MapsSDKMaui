namespace ArcGIS;

public class EmptyPage : ContentPage
{
    public EmptyPage()
    {
        Content = new Label
        {
            Text = "Empty start page",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
    }
}