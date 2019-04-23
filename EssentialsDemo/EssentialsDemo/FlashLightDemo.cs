﻿using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class FlashLightDemo : ContentPage
    {
        Label header;
        Button button1;
        Button button2;
        ScrollView scrollView;

        public FlashLightDemo()
        {
            Title = "Flashlight";

            header = new Label
            {
                Text = "FlashLight",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "On",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1Async;

            button2 = new Button
            {
                Text = "Off",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button2.Clicked += OnButtonClicked2Async;

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { header, button1, button2 }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        async void OnButtonClicked1Async(object sender, EventArgs e)
        {
            try
            {
                // Turn On
                await Flashlight.TurnOnAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine(fnsEx);
                await DisplayAlert("Error", "Handle not supported on device.", "OK");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Console.WriteLine(pEx);
                await DisplayAlert("Error", "Handle permission not given.", "OK");
            }
            catch (Exception ex)
            {
                // Unable to turn on/off flashlight
                Console.WriteLine(ex);
                await DisplayAlert("Error", "Unable to turn on flashlight.", "OK");
            }
        }

        async void OnButtonClicked2Async(object sender, EventArgs e)
        {
            try
            {
                // Turn Off
                await Flashlight.TurnOffAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine(fnsEx);
                await DisplayAlert("Error", "Handle not supported on device.", "OK");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Console.WriteLine(pEx);
                await DisplayAlert("Error", "Handle permission not given.", "OK");
            }
            catch (Exception ex)
            {
                // Unable to turn on/off flashlight
                Console.WriteLine(ex);
                await DisplayAlert("Error", "Unable to turn off flashlight.", "OK");
            }
        }
    }
}