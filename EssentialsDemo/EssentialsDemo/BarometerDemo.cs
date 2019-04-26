using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class BarometerDemo : ContentPage
    {
        // Description of the page
        public string description = "Barometer measures the air pressure around the device in hectopascals.";
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        Button button;
        Label label;
        Label label_description;
        ScrollView scrollView;

        public BarometerDemo()
        {
            Title = "Barometer";

            Label header = new Label
            {
                Text = "Barometer",
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
            Barometer.ReadingChanged += Barometer_ReadingChanged;

            label = new Label
            {
                Text = "",
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
                    Children = { header, button, label, label_description }
                }
            };
            // Build the page.
            this.Content = scrollView;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleBarometer();
            button.Text = String.Format("{0}", Barometer.IsMonitoring == true ? "Stop" : "Start");
        }

        void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
        {
            var data = e.Reading;
            // Process Pressure
            //Console.WriteLine($"Reading: Pressure: {data.PressureInHectopascals} hectopascals");
            label.Text = String.Format("Pressure: {0,0:F2} ㍱", data.PressureInHectopascals);
        }

        public void ToggleBarometer()
        {
            try
            {
                if (Barometer.IsMonitoring)
                {
                    Barometer.Stop();
                    Barometer.ReadingChanged -= Barometer_ReadingChanged;
                }
                else
                {
                    Barometer.Start(speed);
                    Barometer.ReadingChanged += Barometer_ReadingChanged;
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
    }
}