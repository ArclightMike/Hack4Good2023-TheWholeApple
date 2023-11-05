namespace CompanionApp.View;

public partial class AdditionalResources : ContentPage
{
	public AdditionalResources()
	{
		InitializeComponent();
	}

    async void FAQButtonClicked(object sender, EventArgs args)
    {
        try
        {
            Uri uri = new Uri("https://www.gooddads.com/podcast");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
        }
    }
    async void ResourcesButtonClicked(object sender, EventArgs args)
    {
        try
        {
            Uri uri = new Uri("https://www.gooddads.com/about-us");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
        }
    }

    async void NewsletterClicked(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://www.gooddads.com/newsletter");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
        }

    }
}