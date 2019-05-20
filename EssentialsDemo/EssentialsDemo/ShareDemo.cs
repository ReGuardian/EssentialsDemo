using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    class ShareDemo : ContentPage
    {
        public string description = "This function allows users to share data " +
            "to other applications on the device. Usually the content is set by developers. " +
            "In this demo, the content can be set by users.";
        Button button1;
        Button button2;
        Entry text;
        Label label_description;
        ScrollView scrollView;

        public ShareDemo()
        {
            Title = "Share";

            Label header = new Label
            {
                Text = "Share",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Share Text",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            button2 = new Button
            {
                Text = "Share Web Link",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button2.Clicked += OnButtonClicked2;

            text = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter text",
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
                    Children = { header, text, button1, button2, label_description }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        async void OnButtonClicked1(object sender, EventArgs e)
        {
            await ShareText(text.Text);
        }

        async void OnButtonClicked2(object sender, EventArgs e)
        {
            await ShareUri(text.Text);
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/xamarin/essentials/share?context=xamarin%2Fandroid&tabs=android
        /// Xamarin.Essentials: Share, Accessed: 20 May 2019
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/xamarin/essentials/share?context=xamarin%2Fandroid&tabs=android
        /// Xamarin.Essentials: Share, Accessed: 20 May 2019
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link"
            });
        }
    }
}