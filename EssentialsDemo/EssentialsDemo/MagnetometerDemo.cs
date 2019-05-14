using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Microcharts.Forms;
using SkiaSharp;
using Entry = Microcharts.Entry;
using Microcharts;

namespace EssentialsDemo
{
    class MagnetometerDemo : ContentPage
    {
        public string description = "This class gives access to the magnetometer sensor of the device. ";
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        Button button;
        Label label;
        Label label_description;
        ChartView chartView;
        ScrollView scrollView;

        public MagnetometerDemo()
        {
            Title = "Magnetometer";

            Label header = new Label
            {
                Text = "Magnetometer",
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
            Magnetometer.ReadingChanged += Magnetometer_ReadingChanged;

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
            ToggleMagnetometer();
            button.Text = String.Format("{0}", Magnetometer.IsMonitoring == true ? "Stop" : "Start");
        }

        void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            var data = e.Reading;
            // Process MagneticField X, Y, and Z
            Console.WriteLine($"Reading: X: {data.MagneticField.X}, Y: {data.MagneticField.Y}, Z: {data.MagneticField.Z}");
            label.Text = String.Format("X: {0,0:F4} µ\nY: {1,0:F4} µ\nZ: {2,0:F4} µ", data.MagneticField.X, data.MagneticField.Y, data.MagneticField.Z);

            var entries = new[]
            {
                new Entry((float)Math.Abs(Math.Round(data.MagneticField.X, 2)) * 10)
                {
                    Label = "X",
                    ValueLabel = Math.Round(data.MagneticField.X, 2).ToString(),
                    Color = SKColor.Parse("#2c3e50")
                },
                new Entry((float)Math.Abs(Math.Round(data.MagneticField.Y, 2)) * 10)
                {
                    Label = "Y",
                    ValueLabel = Math.Round(data.MagneticField.Y, 2).ToString(),
                    Color = SKColor.Parse("#77d065")
                },
                new Entry((float)Math.Abs(Math.Round(data.MagneticField.Z, 2)) * 10)
                {
                    Label = "Z",
                    ValueLabel = Math.Round(data.MagneticField.Z, 2).ToString(),
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
                MaxValue = 1000,
                MinValue = 0
            };
        }

        public void ToggleMagnetometer()
        {
            try
            {
                if (Magnetometer.IsMonitoring)
                    Magnetometer.Stop();
                else
                    Magnetometer.Start(speed);
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