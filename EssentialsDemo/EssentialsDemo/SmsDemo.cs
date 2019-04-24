using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    class SmsDemo : ContentPage
    {
        Button button1;
        Label label;
        Entry text1;
        Entry text2;
        ScrollView scrollView;
        private Label info;
        private string introduction;

        public SmsDemo()
        {
            Title = "Sms";

            Label header = new Label
            {
                Text = "Sms",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Send",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            text1 = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter message",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            text2 = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter recipient",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            label = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            introduction = "This function allows users to send SMS by calling the origin SMS application of the device.";
            info = new Label { Text = introduction };

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { header, text1, text2, button1, label, info }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        async void OnButtonClicked1(object sender, EventArgs e)
        {
            await SendSms(text1.Text, text2.Text);
        }

        public async Task SendSms(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex);
            }
        }
        /*
        public async Task SendSms(string messageText, string[] recipients)
        {
            try
            {
                var message = new SmsMessage(messageText, recipients);
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex);
            }
        }*/
    }
}