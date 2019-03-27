using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    class BrowserDemo : ContentPage
    {
        Button button1;
        Entry text;
        Uri uriAddress;

        public BrowserDemo()
        {
            Title = "Open Browser";

            Label header = new Label
            {
                Text = "Browser",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Browse",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            text = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter url",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header, text, button1
                }
            };
        }

        void OnButtonClicked1(object sender, EventArgs e)
        {
            uriAddress = new Uri(text.Text, UriKind.RelativeOrAbsolute);
            //System.InvalidOperationException: This operation is not supported for a relative URI.
            //System.UriFormatException: Invalid URI: The format of the URI could not be determine
            OpenBrowser(uriAddress);
        }

        public async void OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}