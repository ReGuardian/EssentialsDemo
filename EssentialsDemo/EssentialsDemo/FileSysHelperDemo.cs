using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class FileSysHelperDemo : ContentPage
    {
        Label header;
        Label label;
        Button button1;
        Button button2;

        public FileSysHelperDemo()
        {
            Title = "File System Helpers";

            header = new Label
            {
                Text = "File System Helper",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Cache Directory",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            button2 = new Button
            {
                Text = "AppData Directory",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button2.Clicked += OnButtonClicked2;

            label = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header, button1, button2, label
                }
            };
        }

        void OnButtonClicked1(object sender, EventArgs e)
        {
            var cacheDir = FileSystem.CacheDirectory;
            Console.WriteLine(cacheDir.ToString());
            label.Text = cacheDir.ToString();

        }

        void OnButtonClicked2(object sender, EventArgs e)
        {
            var mainDir = FileSystem.AppDataDirectory;
            Console.WriteLine(mainDir.ToString());
            label.Text = mainDir.ToString();
        }
    }
}