using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    class BrowserDemo : ContentPage
    {
        //Description of the page
        public string description = "Clicking the button opens the url entered in the entry " +
            "with default system browser. Remember to add \"http://\" before the url, or there will be an error.";
        Button button1;
        Entry text;
        Uri uriAddress;
        Label label_description;
        ScrollView scrollView;

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

            label_description = new Label
            {
                Text = description,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { header, text, button1, label_description }
                }
            };
            // Build the page.
            this.Content = scrollView;
        }

        void OnButtonClicked1(object sender, EventArgs e)
        {
            try
            {
                uriAddress = new Uri(text.Text, UriKind.RelativeOrAbsolute);
                OpenBrowser(uriAddress);
            }
            catch (InvalidOperationException)
            {
                DisplayAlert("Alert", "This operation is not supported for a relative URI.", "OK");
            }
            catch (UriFormatException)
            {
                DisplayAlert("Alert", "The format of the URI could not be determine.", "OK");
            }
            catch (Exception)
            {
                DisplayAlert("Alert", "Error has occured.", "OK");
            }
        }

        public async void OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}