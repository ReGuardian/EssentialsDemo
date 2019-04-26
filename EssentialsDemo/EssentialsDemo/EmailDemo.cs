using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EssentialsDemo
{
    class EmailDemo : ContentPage
    {
        // Description of the page
        public string description = "Clicking the button opens the default email application of the device with content " +
            "of the entries. It means the content of send, to and body will be shown in the default email app.";
        Button button1;
        Label label;
        Entry text1;
        Entry text2;
        Entry text3;
        Label label_description;
        ScrollView scrollView;

        public EmailDemo()
        {
            Title = "Email";

            Label header = new Label
            {
                Text = "Email",
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
                Placeholder = "Subject",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            text2 = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Body",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            text3 = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "To",
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
                    Children = { header, text1, text2, text3, button1, label, label_description }
                }
            };
            // Build the page.
            this.Content = scrollView;
        }

        async void OnButtonClicked1(object sender, EventArgs e)
        {
            List<string> recipients = new List<string>();
            recipients.Add(text3.Text);
            await SendEmail(text1.Text, text2.Text, recipients);
        }

        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Email is not supported on this device
                Console.WriteLine(fnsEx);
                await DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (Exception ex)
            {
                // Some other exception occurred
                Console.WriteLine(ex);
                await DisplayAlert("Error", "Other error has occurred.", "OK");
            }
        }
    }
}