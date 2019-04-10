﻿using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace EssentialsDemo
{
    class DetectShakeDemo : ContentPage
    {
        SensorSpeed speed = SensorSpeed.UI;
        Button button;

        public DetectShakeDemo()
        {
            Title = "Detect Shake";

            Label header = new Label
            {
                Text = "Detect Shake",
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
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header, button
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ToggleAccelerometer();
            button.Text = String.Format("{0}", Accelerometer.IsMonitoring == true ? "Stop" : "Start");
        }

        void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            // Process shake event
            DisplayAlert("Alert", "Shake detected.", "OK");
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