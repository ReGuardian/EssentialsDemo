using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    class PhoneDialerDemo : ContentPage
    {
        public string description = "This function allows users to call a phone number " +
            "by calling the default phone application in the device.";
        Button button1;
        Label label;
        Label label_description;
        Entry text;
        ScrollView scrollView;

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
                    Children = { header, text, button1, label, label_description }
                }
            };

            // Build the page.
            this.Content = scrollView;
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
                DisplayAlert("Error", "Number cannot be null or white space.", "OK");
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                Console.WriteLine(ex);
                DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex);
                DisplayAlert("Error", "Other error has occurred.", "OK");
            }
        }
    }
}