using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class AppInfoDemo : ContentPage
    {
        Button button1;
        Button button2;
        Label label;

        public AppInfoDemo()
        {
            Title = "App Information";
           
            Label header = new Label
            {
                Text = "AppInfo",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Show App Info",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            button2 = new Button
            {
                Text = "Show App Setting Info",
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
            label.Text = ShowAppInfo();
        }

        void OnButtonClicked2(object sender, EventArgs e)
        {
            AppInfo.ShowSettingsUI();
        }

        public String ShowAppInfo()
        {
            // Application Name
            var appName = AppInfo.Name;

            // Package Name/Application Identifier (com.microsoft.testapp)
            var packageName = AppInfo.PackageName;

            // Application Version (1.0.0)
            var version = AppInfo.VersionString;

            // Application Build Number (1)
            var build = AppInfo.BuildString;

            Console.WriteLine($"Reading: App Name: {appName}, Package Name: {packageName}, Version: {version}, " +
                $"App Build Number: {build}");

            String info = "App Name: " + appName + ",\nPackage Name: " + packageName + ",\nApp Version: " + version +
                ",\nApp Build Number: " + build;
            return info;
        }
    }
}