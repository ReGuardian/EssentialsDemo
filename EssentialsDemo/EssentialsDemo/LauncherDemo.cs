using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    class LauncherDemo : ContentPage
    {
        Button button1;
        Entry text;

        public LauncherDemo()
        {
            Title = "Launcher";

            Label header = new Label
            {
                Text = "Launcher",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Launch",
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
                Placeholder = "Enter text",
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

        async void OnButtonClicked1(object sender, EventArgs e)
        {
            Uri uri = new Uri(text.Text);
            try
            {
                //var supportsUri = await Launcher.CanOpenAsync(uri);
                await Launcher.OpenAsync(uri);
            }
            catch (UriFormatException)
            {
                await DisplayAlert("Alert", "The entered uri is not supported.", "OK");
            }
        }
    }
}