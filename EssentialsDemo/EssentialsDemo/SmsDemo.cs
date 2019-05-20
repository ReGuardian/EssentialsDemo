using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    class SmsDemo : ContentPage
    {
        public string description = "This function allows users to send SMS by calling the origin SMS application of the device.";
        Button button1;
        Label label;
        Label label_description;
        Entry text1;
        Entry text2;
        ScrollView scrollView;

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
                    Children = { header, text1, text2, button1, label, label_description }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        async void OnButtonClicked1(object sender, EventArgs e)
        {
            await SendSms(text1.Text, text2.Text);
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/xamarin/essentials/sms?context=xamarin/android
        /// Xamarin.Essentials: SMS, Accessed: 20 May 2019
        /// </summary>
        /// <param name="messageText"></param>
        /// <param name="recipient"></param>
        /// <returns></returns>
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