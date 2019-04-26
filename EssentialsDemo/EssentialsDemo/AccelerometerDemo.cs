using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Diagnostics;

namespace EssentialsDemo
{
    class AccelerometerDemo : ContentPage
    {
        // Description of the page
        public string description = "The accelerometer measures the accerleration applied to " +
                                    "the device against the gravity on a 3d coordinate. " +
                                    "X-axis points horizontally to the right of the device. " +
                                    "Y-axis points vertically to the top of the device. " +
                                    "Z-axis points vertically to the outside of the device. " +
                                    "The picture tries to keep horizontally to the earth to show the dircetion of gravity. " +
                                    "The coefficient in the entry can be adjusted to eliminate the noise. " +
                                    "The coefficient should be an integer larger than zero. " +
                                    "With a larger coefficient, the trembling of the picture becomes smaller " +
                                    "with a higher responding delay.";
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.Fastest;
        ScrollView scrollView;
        Button button;
        Label label;
        Label label_description;
        Entry entry;
        Image image;
        Stopwatch stopWatch;
        long count = 0;

        List<float> list_X = new List<float>();
        List<float> list_Y = new List<float>();
        List<float> list_Z = new List<float>();
        int N = 1;

        public AccelerometerDemo()
        {
            Title = "Accelerometer";
            stopWatch = new Stopwatch();

            Label header = new Label
            {
                Text = "Accelerometer",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            entry = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter filter coefficent",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            button = new Button
            {
                Text = "Start",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };

            image = new Image
            {
                Source = ImageSource.FromResource("EssentialsDemo.regcSharp.jpg"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            button.Clicked += OnButtonClicked;

            label = new Label
            {
                Text = "X:   0.00 G\nY:   0.00 G\nZ:   0.00 G\n",
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
                    Children = { header, entry, button, label, image, label_description }
                }
            };
            // Build the page.
            this.Content = scrollView;

        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleAccelerometer();
            if (Accelerometer.IsMonitoring == true)
            {
                // Register for reading changes, be sure to unsubscribe when finished
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                Console.WriteLine("Register");
            }
            else
            {
                Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
                Console.WriteLine("Over");
            }
            button.Text = String.Format("{0}", Accelerometer.IsMonitoring == true ? "Stop" : "Start");
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            count++;

            // To avoid not able to return on UI thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var data = e.Reading;
                var data_X = data.Acceleration.X;
                var data_Y = data.Acceleration.Y;
                var data_Z = data.Acceleration.Z;

                if (entry.Text != "" && entry.Text != null)
                {
                    N = int.Parse(entry.Text);
                }

                // Control the amount of values in the list to prepare for filtering
                while (list_X.Count > N - 1)
                {
                    list_X.RemoveAt(0);
                }
                list_X.Add(data_X);

                while (list_Y.Count > N - 1)
                {
                    list_Y.RemoveAt(0);
                }
                list_Y.Add(data_Y);

                while (list_Z.Count > N - 1)
                {
                    list_Z.RemoveAt(0);
                }
                list_Z.Add(data_Z);

                data_X = filter(list_X);
                data_Y = filter(list_Y);
                data_Z = filter(list_Z);

                label.Text = String.Format("X: {0,0:+#0.00;- #0.00} G\nY: {1,0:+#0.00;- #0.00} G\nZ: {2,0:+#0.00;- #0.00} G", data_X, data_Y, data_Z);

                var norm = Math.Sqrt(data_X * data_X + data_Y * data_Y + data_Z * data_Z);
                // Calculate the normalized vector
                var x = Math.Round(data.Acceleration.X / norm, 3);
                var y = Math.Round(data.Acceleration.Y / norm, 3);
                var z = Math.Round(data.Acceleration.Z / norm, 3);

                if (y >= 0)
                {
                    image.RotationX = Math.Acos(z / Math.Sqrt(y * y + z * z)) * 180 / Math.PI;
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
            });
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                {
                    Accelerometer.Stop();
                    stopWatch.Stop();
                    TimeSpan timeSpan = stopWatch.Elapsed;
                    long ms = stopWatch.ElapsedMilliseconds;
                    label.Text = timeSpan.ToString() + "; " + count.ToString() + "; " + (count / (ms / 1000)).ToString();
                    stopWatch = new Stopwatch();
                    count = 0;
                }
                else
                {
                    Accelerometer.Start(speed);
                    stopWatch.Start();
                }
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
        /// <summary>
        /// Filter values to look smooth
        /// </summary>
        /// <param name="list"></param>
        /// <returns>average of values</returns>
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