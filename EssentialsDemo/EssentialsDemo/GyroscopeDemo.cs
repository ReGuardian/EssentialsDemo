using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class GyroscopeDemo : ContentPage
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        Button button;
        Label label;

        public GyroscopeDemo()
        {
            Title = "Geolocation";

            Label header = new Label
            {
                Text = "Gyroscope",
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
            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;

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
                    header, button, label
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleGyroscope();
            button.Text = String.Format("{0}", Gyroscope.IsMonitoring == true ? "Stop" : "Start");
        }

        void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;
            // Process Angular Velocity X, Y, and Z reported in rad/s
            Console.WriteLine($"Reading: X: {data.AngularVelocity.X}, Y: {data.AngularVelocity.Y}, Z: {data.AngularVelocity.Z}");
            label.Text = String.Format("X: {0,0:F4} rad/s\nY: {1,0:F4} rad/s\nZ: {2,0:F4} rad/s", data.AngularVelocity.X, data.AngularVelocity.Y, data.AngularVelocity.Z);
        }

        public void ToggleGyroscope()
        {
            try
            {
                if (Gyroscope.IsMonitoring)
                    Gyroscope.Stop();
                else
                    Gyroscope.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                Console.WriteLine(fnsEx);
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex);
            }
        }
    }
}