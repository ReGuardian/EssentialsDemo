﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EssentialsDemo
{
    public class VibrationDemo : ContentPage
    {
        Label header;
        Entry entry;
        Button button1;
        Button button2;
        Button button3;
        ScrollView scrollView;
        public VibrationDemo()
        {
            Title = "Vibrate";

            header = new Label
            {
                Text = "Vibration",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            entry = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter seconds to vibrate",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            button1 = new Button
            {
                Text = "Vibrate with default",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            button2 = new Button
            {
                Text = "Vibrate with seconds",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button2.Clicked += OnButtonClicked2;

            button3 = new Button
            {
                Text = "Cancel",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button3.Clicked += OnButtonClicked3;

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { header, button1, entry, button2, button3 }
                }
            };

            Content = scrollView;
        }

        void OnButtonClicked1(object sender, EventArgs e)
        {
            try
            {
                // Use default vibration length
                Vibration.Vibrate();
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
                Console.WriteLine(ex);
                DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex);
                DisplayAlert("Error", "Other error has occurred.", "OK");
            }
        }
        void OnButtonClicked2(object sender, EventArgs e)
        {
            try
            {
                // Use specified vibration time
                var duration = TimeSpan.FromSeconds(Convert.ToDouble(entry.Text));
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
                Console.WriteLine(ex);
                DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex);
                DisplayAlert("Error", "Other error has occurred.", "OK");
            }
        }

        void OnButtonClicked3(object sender, EventArgs e)
        {
            try
            {
                Vibration.Cancel();
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
                Console.WriteLine(ex);
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