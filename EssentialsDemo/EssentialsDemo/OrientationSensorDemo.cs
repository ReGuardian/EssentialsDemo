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

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header, button, image, label
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
            float Data_X = data.Orientation.X;
            float Data_Y = data.Orientation.Y;
            float Data_Z = data.Orientation.Z;
            float Data_W = data.Orientation.W;
            var θ = 2 * Math.Acos(Data_W);
            Console.WriteLine(θ * 180 / Math.PI);
            // 旋转轴
            var ax = Data_X / Math.Sin(θ / 2);
            var ay = Data_Y / Math.Sin(θ / 2);
            var az = Data_Z / Math.Sin(θ / 2);
            // 手机平面的单位法向量，初始值为（a，b，c）=（0，0，1）
            var a = 0;
            var b = 0;
            var c = 1;
            var x = a * (Math.Cos(θ) + ax * ax * (1 - Math.Cos(θ)))
                + b * (ax * ay * (1 - Math.Cos(θ)) - az * Math.Sin(θ))
                + c * (ax * az * (1 - Math.Cos(θ)) + ay * Math.Sin(θ));
            var y = a * (ax * ay * (1 - Math.Cos(θ)) + az * Math.Sin(θ))
                + b * (Math.Cos(θ) + ay * ay * (1 - Math.Cos(θ)))
                + c * (ay * az * (1 - Math.Cos(θ)) - ax * Math.Sin(θ));
            var z = a * (ax * az * (1 - Math.Cos(θ)) - ay * Math.Sin(θ))
                + b * (ay * az * (1 - Math.Cos(θ)) + ax * Math.Sin(θ))
                + c * (Math.Cos(θ) + az * az * (1 - Math.Cos(θ)));

            // Process Orientation quaternion (X, Y, Z, and W)
            //Console.WriteLine($"Reading: X: {data.Orientation.X}, Y: {data.Orientation.Y}, Z: {data.Orientation.Z}, W: {data.Orientation.W}");
            label.Text = String.Format("X: {0,0:F4} \nY: {1,0:F4} \nZ: {2,0:F4} \nW: {3,0:F4}",
                Data_X, Data_Y, Data_Z, Data_W);


            //if (z >= 0)
            //{
            //    image.Rotation = Math.Acos(Math.Abs(x) / Math.Sqrt(x * x + y * y)) * 180 / Math.PI;
            //    Console.WriteLine("rot: " + image.Rotation);
            //}
            //else
            //{
            //    image.Rotation = 360 - Math.Acos(Math.Abs(x) / Math.Sqrt(x * x + y * y)) * 180 / Math.PI;
            //    Console.WriteLine("rot: " + image.Rotation);
            //}
            if (y >= 0)
            {
                image.RotationX = Math.Acos(y / Math.Sqrt(y * y + z * z)) * 180 / Math.PI;
                //Console.WriteLine(Math.Acos(y / Math.Sqrt(y * y + z * z)) * 180 / Math.PI);
            }
            else
            {
                image.RotationX = 360 - Math.Acos(y / Math.Sqrt(y * y + z * z)) * 180 / Math.PI;
            }
            if (x >= 0)
            {
                image.RotationY = Math.Acos(x / Math.Sqrt(x * x + z * z)) * 180 / Math.PI;
                Console.WriteLine(image.RotationY);
            }
            else
            {
                image.RotationY = 360 - Math.Acos(x / Math.Sqrt(x * x + z * z)) * 180 / Math.PI;
                Console.WriteLine(image.RotationY);
            }
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