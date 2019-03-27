using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    class PhoneDialerDemo : ContentPage
    {
        Button button1;
        Label label;
        Entry text;
        Label exception;

        public PhoneDialerDemo()
        {
            Title = "Phone Dialer";

            Label header = new Label
            {
                Text = "PhoneDialer",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Dial",
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
                Placeholder = "Enter phone number",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            label = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            exception = new Label
            {
                Text = "",
                TextColor = Color.Red,
                HorizontalOptions = LayoutOptions.End
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header, text, button1, label, exception
                }
            };
        }

        void OnButtonClicked1(object sender, EventArgs e)
        {
            PlacePhoneCall(text.Text);
        }

        public void PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space.
                Console.WriteLine(anEx);
                exception.Text = "Number cannot be null or white space";
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                Console.WriteLine(ex);
                exception.Text = "Phone Dialer is not supported on this device";
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex);
                exception.Text = "Other error has occurred.";
            }
        }
    }
}