using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class ClipboardDemo : ContentPage
    {
        // Description of the page
        public string description = "Clicking the set button pastes the text in the entry to the clipboard." +
            "Clicking the get button shows the content in the clipboard.";
        Button button1;
        Button button2;
        Label label;
        Label label_description;
        Entry text;
        ScrollView scrollView;

        public ClipboardDemo()
        {
            Title = "Clipboard";

            Label header = new Label
            {
                Text = "Clipboard",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Set to clipboard",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            button2 = new Button
            {
                Text = "Get from clipboard",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button2.Clicked += OnButtonClicked2Async;

            text = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter text",
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
                    Children = { header, text, button1, button2, label, label_description }
                }
            };
            // Build the page.
            this.Content = scrollView;
        }

        private void Clipboard_EnergySaverStatusChanged(object sender, EnergySaverStatusChangedEventArgs e)
        {
            // Process change
            var status = e.EnergySaverStatus;
            Console.WriteLine($"Reading: Status: {status}");
        }

        async void OnButtonClicked1(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(text.Text);
        }

        async void OnButtonClicked2Async(object sender, EventArgs e)
        {
            label.Text = await ShowClipboardTextAsync();
        }

        public async System.Threading.Tasks.Task<string> ShowClipboardTextAsync()
        {
            var text = await Clipboard.GetTextAsync();

            Console.WriteLine($"Reading: Text: {text}");

            String info = "Text in the clipboard: " + text;
            return info;
        }
    }
}