using ArcGIS.Controls;
using ArcGIS.Samples.Managers;
using ArcGIS.Samples.Shared.Managers;

namespace ArcGIS;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        this.Appearing += OnFirstAppearing;
    }

    private async void OnFirstAppearing(object? sender, EventArgs e)
    {
        this.Appearing -= OnFirstAppearing;

        // Let the first frame appear (avoids the scene-create watchdog)
        await Task.Yield();

        // Now do the heavy work
        try
        {
            SampleManager.Current.Initialize();

            // Put the real flyout content in
            FlyoutHost.Content = new CategoriesFlyoutContent();

            // Navigate to the real starting page
            await GoToAsync("//CategoryPage");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Startup error: {ex}");
        }

        // Optional: keep your original API-key check
        _ = CheckApiKey();
    }

    private async Task CheckApiKey()
    {
        await ApiKeyManager.TrySetLocalKey();
        var status = await ApiKeyManager.CheckKeyValidity();
        if (status != ApiKeyStatus.Valid)
        {
            await Shell.Current.Navigation.PushAsync(new ApiKeyPage(), true);
        }
    }
}