using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class OrientationSensorDemo : ContentPage
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        Button button;
        Image image;
        Label label;
        Label exception;

        public OrientationSensorDemo()
        {
            Title = "Orientation Sensor";

            Label header = new Label
            {
                Text = "OrientationSensor",
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
            OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;

            image = new Image { Source = ImageSource.FromResource("EssentialsDemo.regcSharp.jpg") };

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
                    header, button, image, label, exception
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
            var data = e.Reading;
            // Process Orientation quaternion (X, Y, Z, and W)
            //Console.WriteLine($"Reading: X: {data.Orientation.X}, Y: {data.Orientation.Y}, Z: {data.Orientation.Z}, W: {data.Orientation.W}");
            label.Text = String.Format("X: {0,0:F4} \nY: {1,0:F4} \nZ: {2,0:F4} \nW: {3,0:F4}", 
                data.Orientation.X, data.Orientation.Y, data.Orientation.Z, data.Orientation.W);

            var θ = 2 * Math.Acos(data.Orientation.W);
            Console.WriteLine(θ * 180 / Math.PI);
            var x = data.Orientation.X / Math.Sin(θ / 2);
            var y = data.Orientation.Y / Math.Sin(θ / 2);
            var z = data.Orientation.Z / Math.Sin(θ / 2);

            if (z >= 0)
            {
                image.Rotation = Math.Acos(Math.Abs(x) / Math.Sqrt(x * x + y * y)) * 180 / Math.PI;
                Console.WriteLine("rot: " + image.Rotation);
            }
            else
            {
                image.Rotation = 360 - Math.Acos(Math.Abs(x) / Math.Sqrt(x * x + y * y)) * 180 / Math.PI;
                Console.WriteLine("rot: " + image.Rotation);
            }
            //if (y >= 0)
            //{
            //    image.RotationX = Math.Acos(y / Math.Sqrt(y * y + z * z)) * 180 / Math.PI;
            //    //Console.WriteLine(Math.Acos(y / Math.Sqrt(y * y + z * z)) * 180 / Math.PI);
            //}
            //else
            //{
            //    image.RotationX = 360 - Math.Acos(y / Math.Sqrt(y * y + z * z)) * 180 / Math.PI;
            //}
            //if (x >= 0)
            //{
            //    image.RotationY = Math.Acos(x / Math.Sqrt(x * x + z * z)) * 180 / Math.PI;
            //    Console.WriteLine(image.RotationY);
            //}
            //else
            //{
            //    image.RotationY = 360 - Math.Acos(x / Math.Sqrt(x * x + z * z)) * 180 / Math.PI;
            //    Console.WriteLine(image.RotationY);
            //}
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