using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class CompassDemo : ContentPage
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.Game;
        Button button;
        Label label;
        Label label2;
        Label exception;
        Image image;

        public CompassDemo()
        {
            Title = "Compass";

            Label header = new Label
            {
                Text = "Compass",
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

            image = new Image
            {
                Source = ImageSource.FromResource("EssentialsDemo.compass.png"),
                //https://www.flaticon.com/free-icon/compass_1553376#term=compass north&page=1&position=30
                Scale = 1
            };

            button.Clicked += OnButtonClicked;
            // Register for reading changes, be sure to unsubscribe when finished
            Compass.ReadingChanged += Compass_ReadingChanged;

            label = new Label
            {
                Text = "HeadingMagneticNorth:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            label2 = new Label
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
                    header, button, image, label, label2, exception
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleCompass();
            button.Text = String.Format("{0}", Compass.IsMonitoring == true ? "Stop" : "Start");
        }

        void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            //To avoid not able to return on UI thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var data = e.Reading;
                // Process Heading Magnetic North
                //Console.WriteLine($"Reading: {data.HeadingMagneticNorth} degrees");
                label2.Text = String.Format("{0,0:000} °", data.HeadingMagneticNorth);
                image.Rotation = data.HeadingMagneticNorth;
            });
        }

        public void ToggleCompass()
        {
            try
            {
                if (Compass.IsMonitoring)
                    Compass.Stop();
                else
                    Compass.Start(speed);
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