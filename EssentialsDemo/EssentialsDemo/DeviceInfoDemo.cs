using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class DeviceInfoDemo : ContentPage
    {
        Button button1;
        Label label;
        ScrollView scrollView;

        public DeviceInfoDemo()
        {
            Title = "Device Information";

            Label header = new Label
            {
                Text = "DeviceInfo",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Show DeviceInfo Info",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            label = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = label
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header, button1, scrollView
                }
            };
        }

        void OnButtonClicked1(object sender, EventArgs e)
        {
            label.Text = ShowDeviceInfo();
        }

        public String ShowDeviceInfo()
        {
            // Device Model (SMG-950U, iPhone10,6)
            var device = DeviceInfo.Model;

            // Manufacturer (Samsung)
            var manufacturer = DeviceInfo.Manufacturer;

            // Device Name (Motz's iPhone)
            var deviceName = DeviceInfo.Name;

            // Operating System Version Number (7.0)
            var version = DeviceInfo.VersionString;

            // Platform (Android)
            var platform = DeviceInfo.Platform;

            // Idiom (Phone)
            var idiom = DeviceInfo.Idiom;

            // Device Type (Physical)
            var deviceType = DeviceInfo.DeviceType;

            Console.WriteLine($"Reading: Device Model: {device}, Manufacture: {manufacturer}, Device Name: {deviceName}, " +
                $"OS Version: {version}, Platform: {platform}, Idiom: {idiom}, Device Type: {deviceType}");

            String info = $"Device Model: {device}\n" + 
                          $"Manufacture: {manufacturer}\n" + 
                          $"Device Name: {deviceName}\n" +
                          $"OS Version: {version}\n" + 
                          $"Platform: {platform}\n" +
                          $"Idiom: {idiom}\n" + 
                          $"Device Type: {deviceType}\n";
            return info;
        }
    }
}