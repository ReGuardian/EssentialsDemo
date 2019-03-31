using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class AccelerometerDemo : ContentPage
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        Button button;
        Label label;
        Label exception;
        Image image;

        public AccelerometerDemo()
        {
            Title = "Accelerometer";

            Label header = new Label
            {
                Text = "Accelerometer",
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

            image = new Image { Source = ImageSource.FromResource("EssentialsDemo.regcSharp.jpg") };

            button.Clicked += OnButtonClicked;
            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;

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
                    header, button, label, image, exception
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleAccelerometer();
            button.Text = String.Format("{0}", Accelerometer.IsMonitoring == true ? "Stop" : "Start");
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            //Console.WriteLine($"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");
            label.Text = String.Format("X: {0,0:F4} G\nY: {1,0:F4} G\nZ: {2,0:F4} G", data.Acceleration.X, data.Acceleration.Y, data.Acceleration.Z);
            // Process Acceleration X, Y, and Z

            var norm = Math.Sqrt(data.Acceleration.X * data.Acceleration.X + data.Acceleration.Y * data.Acceleration.Y + data.Acceleration.Z * data.Acceleration.Z);
            var x = data.Acceleration.X / norm;
            var y = data.Acceleration.Y / norm;
            var z = data.Acceleration.Z / norm;
            if (y >= 0)
            {
                image.RotationX = Math.Acos(z / Math.Sqrt(y * y + z * z)) * 180 / Math.PI;
                //Console.WriteLine(Math.Acos(z / Math.Sqrt(y * y + z * z)) * 180 / Math.PI);
            }
            else
            {
                image.RotationX = 360 - Math.Acos(z / Math.Sqrt(y * y + z * z)) * 180 / Math.PI;
            }
            if (x >= 0)
            {
                image.RotationY = Math.Acos(z / Math.Sqrt(x * x + z * z)) * 180 / Math.PI;
            }
            else
            {
                image.RotationY = 360 - Math.Acos(z / Math.Sqrt(x * x + z * z)) * 180 / Math.PI;
            }
            image.Scale = norm;
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
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