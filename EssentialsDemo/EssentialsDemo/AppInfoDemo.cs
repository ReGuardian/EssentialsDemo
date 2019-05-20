using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class AppInfoDemo : ContentPage
    {
        // Description of the page
        public string description = "Clicking the first button shows the information " +
            "of the application. Clicking the other one turns to the default app setting page of the device. ";
        Button button1;
        Button button2;
        Label label;
        Label label_description;
        ScrollView scrollView;

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
                    Children = {header, button1, button2, label, label_description}
                }
            };
            // Build the page.
            this.Content = scrollView;
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
            /*
             * https://docs.microsoft.com/en-us/xamarin/essentials/app-information?context=xamarin%2Fandroid&tabs=android
             * Xamarin Essentials: App Information, Accessed: 20 May 2019
             */
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