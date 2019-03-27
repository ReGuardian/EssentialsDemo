using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class BarometerDemo : ContentPage
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        Button button;
        Label label;
        Label exception;

        public BarometerDemo()
        {
            Title = "Barometer";

            Label header = new Label
            {
                Text = "Barometer",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button = new Button
            {
                Text = "Start",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };

            button.Clicked += OnButtonClicked;
            // Register for reading changes, be sure to unsubscribe when finished
            Barometer.ReadingChanged += Barometer_ReadingChanged;

            label = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            exception = new Label
            {
                Text = "",
                TextColor = Color.Red,
                HorizontalOptions = LayoutOptions.End
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header, button, label, exception
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleBarometer();
            button.Text = String.Format("{0}", Barometer.IsMonitoring == true ? "Stop" : "Start");
        }

        void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
        {
            var data = e.Reading;
            // Process Pressure
            Console.WriteLine($"Reading: Pressure: {data.PressureInHectopascals} hectopascals");
            label.Text = String.Format("Pressure: {0,0:F4}", data.PressureInHectopascals);
        }

        public void ToggleBarometer()
        {
            try
            {
                if (Barometer.IsMonitoring)
                    Barometer.Stop();
                else
                    Barometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                Console.WriteLine(fnsEx);
                exception.Text = "Feature not supported on device";
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex);
                exception.Text = "Other error has occurred";
            }
        }
    }
}