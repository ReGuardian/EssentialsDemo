using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class DeviceDisplayDemo : ContentPage
    {
        Button button1;
        Button button2;
        Label label;

        public DeviceDisplayDemo()
        {
            Title = "Device Display Information";

            Label header = new Label
            {
                Text = "DeviceDisplay",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Show DeviceDisplay Info",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            button2 = new Button
            {
                Text = "Screen Lock",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button2.Clicked += OnButtonClicked2;

            // Subscribe to changes of screen metrics
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;

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
            label.Text = ShowDeviceDisplayInfo();
        }

        void OnButtonClicked2(object sender, EventArgs e)
        {
            label.Text = ToggleScreenLock();
        }

        void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            // Process changes
            var displayInfo = e.DisplayInfo;
            Console.WriteLine($"DisplayInfo: {displayInfo}");
        }

        public String ShowDeviceDisplayInfo()
        {
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Orientation (Landscape, Portrait, Square, Unknown)
            var orientation = mainDisplayInfo.Orientation;

            // Rotation (0, 90, 180, 270)
            var rotation = mainDisplayInfo.Rotation;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;

            // Screen density
            var density = mainDisplayInfo.Density;

            Console.WriteLine($"Reading: Orientation: {orientation}, Rotation: {rotation}, Width: {width}, Height: {height}, Density: {density}");

            String info = $"Orientation: {orientation}\n" + 
                          $"Rotation: {rotation}\n" + 
                          $"Width: {width}\n" + 
                          $"Height: {height}\n" + 
                          $"Density: {density}";
            return info;
        }

        public string ToggleScreenLock()
        {
            DeviceDisplay.KeepScreenOn = !DeviceDisplay.KeepScreenOn;
            if (DeviceDisplay.KeepScreenOn)
            {
                return "The device display is kept from turning off or locking";
            }
            else
            {
                return "The device display will be turned off afterwards";
            }
        }

    }
}