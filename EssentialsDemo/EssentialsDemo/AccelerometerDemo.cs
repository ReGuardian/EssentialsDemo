using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace EssentialsDemo
{
    class AccelerometerDemo : ContentPage
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.Game;
        Button button;
        Label label;
        Label exception;
        Image image;
        Label result1;
        Label result2;

        float x0 = -100;
        float y0 = -100;
        float z0 = -100;

        List<float> list_X = new List<float>();
        List<float> list_Y = new List<float>();
        List<float> list_Z = new List<float>();
        int N = 5;

        public AccelerometerDemo()
        {
            Title = "Accelerometer";

            result1 = new Label();
            result2 = new Label();

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
                    header, button, label, image, result1, result2, exception
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
            var data_X = data.Acceleration.X;
            var data_Y = data.Acceleration.Y;
            var data_Z = data.Acceleration.Z;

            //************************low pass****************
            //var a = 0.5;
            //var b = 1 - a;

            //if (x0 == -100)
            //{
            //    x0 = data_X;
            //}
            //if (y0 == -100)
            //{
            //    y0 = data_Y;
            //}
            //if (z0 == -100)
            //{
            //    z0 = data_Z;
            //}
            //data_X = (float)(a * x0 + b * data_X);
            //data_Y = (float)(a * y0 + b * data_Y);
            //data_Z = (float)(a * z0 + b * data_Z);
            //*******************low pass***********************

            //*********************avg******************
            if (list_X.Count > N - 1)
            {
                list_X.RemoveAt(0);
            }
            list_X.Add(data_X);

            if (list_Y.Count > N - 1)
            {
                list_Y.RemoveAt(0);
            }
            list_Y.Add(data_Y);

            if (list_Z.Count > N - 1)
            {
                list_Z.RemoveAt(0);
            }
            list_Z.Add(data_Z);

            data_X = filter(list_X);
            data_Y = filter(list_Y);
            data_Z = filter(list_Z);
            //*******************avg*******************

            //Console.WriteLine($"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");
            label.Text = String.Format("X: {0,0:F4} G\nY: {1,0:F4} G\nZ: {2,0:F4} G", data_X, data_Y, data_Z);
            // Process Acceleration X, Y, and Z

            var norm = Math.Sqrt(data_X * data_X + data_Y * data_Y + data_Z * data_Z);
            //var x = Math.Round(data_X / norm, 2);
            //var y = Math.Round(data_Y / norm, 2);
            //var z = Math.Round(data_Z / norm, 2);
            //norm = Math.Round(norm, 2);
            var x = data_X / norm;
            var y = data_Y / norm;
            var z = data_Z / norm;


            var Rx1 = Math.Acos(z / Math.Sqrt(y * y + z * z)) * 180 / Math.PI;
            var Rx2 = Math.Acos(Math.Abs(z) / Math.Sqrt(y * y + z * z)) * 180 / Math.PI; ;
            var Ry1 = Math.Acos(z / Math.Sqrt(x * x + z * z)) * 180 / Math.PI;
            var Ry2 = Math.Acos(Math.Abs(z) / Math.Sqrt(x * x + z * z)) * 180 / Math.PI;

            // still has problem
            if (y >= 0)
            {
                if (z >= 0)
                {
                    image.RotationX = Rx1;
                }
                else
                {
                    image.RotationX = Rx1;
                }
            }
            // y < 0
            else
            {
                if (z >= 0)
                {
                    image.RotationX = 360 - Rx1;
                }
                else
                {
                    image.RotationX = 360 - Rx2;
                }
            }
            if (x >= 0)
            {
                if (z >= 0)
                {
                    image.RotationY = Ry1;
                }
                else
                {
                    image.RotationY = Ry2;
                }
            }
            // x < 0
            else
            {
                if (z >= 0)
                {
                    image.RotationY = 360 - Ry1;
                }
                else
                {
                    image.RotationY = 360 - Ry2;
                }
            }

            image.Scale = norm;

            result1.Text = "Rx: " + image.RotationX;
            result2.Text = "Ry: " + image.RotationY;
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

        private float filter(List<float> list)
        {
            float sum = 0;
            foreach (float element in list)
            {
                sum += element;
            }
            return sum / list.Count;
        }
    }
}