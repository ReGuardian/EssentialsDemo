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
        double zero = 0.0;
        double delta = 0.0;
        Button button;
        Image image;
        Label label;
        Label label2;
        ScrollView scrollView;
        Button button_zero;
        private Label info;
        private string introduction;

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

            button_zero = new Button
            {
                Text = "Zero",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };

            button.Clicked += OnButtonClicked;
            button_zero.Clicked += OnButton_zeroClicked;
            // Register for reading changes, be sure to unsubscribe when finished
            OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;

            image = new Image
            {
                Source = ImageSource.FromResource("EssentialsDemo.compass.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            label = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            introduction = "This class gives access to the orientation sensor of the device.\n" +
                "The device (generally a phone or tablet) has a 3D coordinate system with the following axes:\n" +
                "·The positive X axis points to the right of the display in portrait mode.\n" +
                "·The positive Y axis points to the top of the device in portrait mode.\n" +
                "·The positive Z axis points out of the screen.\n" +
                "The 3D coordinate system of the Earth has the following axes:\n" +
                "·The positive X axis is tangent to the surface of the Earth and points east.\n" +
                "·The positive Y axis is also tangent to the surface of the Earth and points north.\n" +
                "·The positive Z axis is perpendicular to the surface of the Earth and points up.\n" +
                "(X, Y, Z, W) describes the rotation of the device's coordinate system relative to a certian coordinate system.\n" +
                "On Android, it refers to the Earth's coordinate system; on iOS, it refers to the following coordinate system:\n" +
                "·The positive X axis points to the right of the display in portrait mode.\n" +
                "·The positive Y axis is also tangent to the surface of the Earth and points to the top of the display in portrait mode.\n" +
                "·The positive Z axis is perpendicular to the surface of the Earth and points up.\n" +
                " If an axis of rotation is the normalized vector (ax, ay, az), and the rotation angle is Θ, then the (X, Y, Z, W) components of the quaternion are:\n" +
                "(ax·sin(Θ/2), ay·sin(Θ/2), az·sin(Θ/2), cos(Θ/2))\n" +
                "This demo has made certian configuration to the picture so that this picture would look as if it were staying still. (On Android, it would serve as a 3-D compass)";
            info = new Label { Text = introduction };

            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                scrollView = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Children = { header, button, label2, image, label, info }
                    }
                };
            }
            else
            {
                scrollView = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Children = { header, button, button_zero, label2, image, label, info }
                    }
                };
            }

            // Build the page.
            this.Content = scrollView;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleOrientationSensor();
            button.Text = String.Format("{0}", OrientationSensor.IsMonitoring == true ? "Stop" : "Start");
        }

        void OnButton_zeroClicked(object sender, EventArgs e)
        {
            delta = zero;
            image.Rotation -= delta;
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

                zero = α;

                image.Rotation = α - delta;
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