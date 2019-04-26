using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class BatteryDemo : ContentPage
    {
        // Description of the page
        public string description = "Clicking the button shows the information on battery. " +
            "Level is a double value between 0 and 1. " +
            "State has values of charging, discharging, not charging, full, not present and unknown." +
            "Source value can be battery, AC, usb, wireless and unknown." +
            "Status is status of energy saver, the value can be on or off or unknown.";
        Button button;
        Label label;
        Label label_description;
        ScrollView scrollView;

        public BatteryDemo()
        {
            Title = "Battery";

            Label header = new Label
            {
                Text = "Battery",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button = new Button
            {
                Text = "Show Battery Info",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };

            button.Clicked += OnButtonClicked;
            // Register for battery changes, be sure to unsubscribe when needed
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            // Subscribe to changes of energy-saver status
            Battery.EnergySaverStatusChanged += Battery_EnergySaverStatusChanged;

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
                    Children = { header, button, label, label_description }
                }
            };
            // Build the page.
            this.Content = scrollView;
        }

        private void Battery_EnergySaverStatusChanged(object sender, EnergySaverStatusChangedEventArgs e)
        {
            // Process change
            var status = e.EnergySaverStatus;
            Console.WriteLine($"Reading: Status: {status}");
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            label.Text = ShowBatteryInfo();
        }

        void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            var level = e.ChargeLevel;
            var state = e.State;
            var source = e.PowerSource;
            Console.WriteLine($"Reading: Level: {level}, State: {state}, Source: {source}");
            label.Text = ShowBatteryInfo();
        }

        public String ShowBatteryInfo()
        {
            var level = Battery.ChargeLevel; // returns 0.0 to 1.0 or 1.0 when on AC or no battery.

            var state = Battery.State;

            switch (state)
            {
                case BatteryState.Charging:
                    // Currently charging
                    break;
                case BatteryState.Full:
                    // Battery is full
                    break;
                case BatteryState.Discharging:
                case BatteryState.NotCharging:
                    // Currently discharging battery or not being charged
                    break;
                case BatteryState.NotPresent:
                // Battery doesn't exist in device (desktop computer)
                case BatteryState.Unknown:
                    // Unable to detect battery state
                    break;
            }

            var source = Battery.PowerSource;

            switch (source)
            {
                case BatteryPowerSource.Battery:
                    // Being powered by the battery
                    break;
                case BatteryPowerSource.AC:
                    // Being powered by A/C unit
                    break;
                case BatteryPowerSource.Usb:
                    // Being powered by USB cable
                    break;
                case BatteryPowerSource.Wireless:
                    // Powered via wireless charging
                    break;
                case BatteryPowerSource.Unknown:
                    // Unable to detect power source
                    break;
            }

            // Get energy saver status
            var status = Battery.EnergySaverStatus;

            Console.WriteLine($"Reading: Level: {level}, State: {state}, Source: {source}, Status: {status}");

            String info = "Level: " + level + ",\nState: " + state + ",\nSource: " + source + ",\nStatus: " + status;
            return info;
        }
    }
}