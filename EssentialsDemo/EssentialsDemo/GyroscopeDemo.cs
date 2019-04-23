using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Microcharts;
using Microcharts.Forms;
using Entry = Microcharts.Entry;
using System.Collections.Generic;
using SkiaSharp;

namespace EssentialsDemo
{
    class GyroscopeDemo : ContentPage
    {
        // Description of the page
        public string description = "Clicking the button starts/stops measuring rotation rate in rad/s around X,Y,Z axis. " +
            "X-axis points horizontally to the right of the device. " +
            "Y-axis points vertically to the top of the device." +
            "Z-axis points vertically outside off the device. " +
            "Their absolute values composes the radar chart. ";
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.Game;
        Button button;
        Label label;
        ChartView chartView;
        Label label_description;
        ScrollView scrollView;

        List<float> list_X = new List<float>();
        List<float> list_Y = new List<float>();
        List<float> list_Z = new List<float>();
        int N = 10;

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
                VerticalOptions = LayoutOptions.Center,
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
                VerticalOptions = LayoutOptions.Center
            };

            chartView = new ChartView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            label_description = new Label
            {
                Text = description,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { header, button, label, chartView, label_description }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleGyroscope();
            button.Text = String.Format("{0}", Gyroscope.IsMonitoring == true ? "Stop" : "Start");
        }

        void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Reading data
                var data = e.Reading;
                var data_X = data.AngularVelocity.X;
                var data_Y = data.AngularVelocity.Y;
                var data_Z = data.AngularVelocity.Z;

                // Control the amount of values in the list to prepare for filtering
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

                // Filtered data
                data_X = filter(list_X);
                data_Y = filter(list_Y);
                data_Z = filter(list_Z);

                // Process Angular Velocity X, Y, and Z reported in rad/s
                //Console.WriteLine($"Reading: X: {data_X}, Y: {data_Y}, Z: {data_Z}");
                label.Text = String.Format("X: {0,0:+#0.00;- #0.00} rad/s\nY: {1,0:+#0.00;- #0.00} rad/s\nZ: {2,0:+#0.00;- #0.00} rad/s",
                    data_X, data_Y, data_Z);
                var entries = new[]
                {
                     new Entry((float)Math.Abs(Math.Round(data_X, 2)) * 10)
                     {
                         Label = "X",
                         ValueLabel = Math.Round(data_X, 2).ToString(),
                         Color = SKColor.Parse("#2c3e50")
                     },
                     new Entry((float)Math.Abs(Math.Round(data_Y, 2)) * 10)
                     {
                         Label = "Y",
                         ValueLabel = Math.Round(data_Y, 2).ToString(),
                         Color = SKColor.Parse("#77d065")
                     },
                     new Entry((float)Math.Abs(Math.Round(data_Z, 2)) * 10)
                     {
                         Label = "Z",
                         ValueLabel = Math.Round(data_Z, 2).ToString(),
                         Color = SKColor.Parse("#b455b6")
                     }
                };
                chartView.Chart = new RadarChart()
                {
                    Entries = entries,
                    LabelTextSize = 35,
                    LineSize = 3,
                    BorderLineSize = 3,
                    PointSize = 10,
                    MaxValue = 30,
                    MinValue = 0
                };
            });
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