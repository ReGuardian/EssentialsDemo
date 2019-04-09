using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Numerics;

namespace EssentialsDemo
{
    class OrientationSensorDemo : ContentPage
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.Game;
        Button button;
        Image image;
        Label label;
        Label label2;

        public OrientationSensorDemo()
        {
            Title = "Orientation Sensor";

            Label header = new Label
            {
                Text = "Orientation Sensor",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            label2 = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.Blue
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
            OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;

            image = new Image { Source = ImageSource.FromResource("EssentialsDemo.compass.png") };

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
                    header, button, label2, image, label
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleOrientationSensor();
            button.Text = String.Format("{0}", OrientationSensor.IsMonitoring == true ? "Stop" : "Start");
        }

        void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            //To avoid not able to return on UI thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var data = e.Reading;
                var θ = 2 * Math.Acos(data.Orientation.W) * 180 / Math.PI;
                label2.Text = String.Format("{0, 0:#0°}", θ);
                // Process Orientation quaternion (X, Y, Z, and W)
                //Console.WriteLine($"Reading: X: {data.Orientation.X}, Y: {data.Orientation.Y}, Z: {data.Orientation.Z}, W: {data.Orientation.W}");
                label.Text = String.Format("X: {0,0:+#0.00;- #0.00} G  Y: {1,0:+#0.00;- #0.00} G\n" +
                    "Z: {2,0:+#0.00;- #0.00} G W: {3,0:+#0.00;- #0.00} G",
                    data.Orientation.X, data.Orientation.Y, data.Orientation.Z, data.Orientation.W);
                
                var x = data.Orientation.X;
                var y = data.Orientation.Y;
                var z = data.Orientation.Z;
                var w = data.Orientation.W;

                // https://www.cnblogs.com/21207-iHome/p/6894128.html
                var α = Math.Atan2(2 * (x * y + z * w), 1 - 2 * (y * y + z * z)) * 180 / Math.PI;
                var β = Math.Asin(2 * (x * z - y * w)) * 180 / Math.PI;
                var γ = Math.Atan2(2 * (x * w + y * z), 1 - 2 * (z * z + w * w)) * 180 / Math.PI;

                image.Rotation = α;
                image.RotationY = β;
                image.RotationX = -γ - 180;
            });
        }

        public void ToggleOrientationSensor()
        {
            try
            {
                if (OrientationSensor.IsMonitoring)
                    OrientationSensor.Stop();
                else
                    OrientationSensor.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                Console.WriteLine(fnsEx);
                DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex);
                DisplayAlert("Error", "Other error has occurred.", "OK");
            }
        }
    }
}