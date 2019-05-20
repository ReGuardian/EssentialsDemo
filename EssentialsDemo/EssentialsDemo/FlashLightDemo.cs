﻿using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class FlashLightDemo : ContentPage
    {
        // Description of the page
        public string description = "Clicking the on/off button switches on/off the flashlight of the device. " +
            "Permissions maybe asked when clicking the buttons. ";
        Label header;
        Button button1;
        Button button2;
        Label label_description;
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
                    Children = { header, button1, button2, label_description }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        async void OnButtonClicked1Async(object sender, EventArgs e)
        {
            /*
             * https://docs.microsoft.com/en-us/xamarin/essentials/flashlight?context=xamarin%2Fandroid&tabs=android
             * Xamarin.Essentials: Flashlight, Accessed: 20 May 2019
             */
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
            /*
             * https://docs.microsoft.com/en-us/xamarin/essentials/flashlight?context=xamarin%2Fandroid&tabs=android
             * Xamarin.Essentials: Flashlight, Accessed: 20 May 2019
             */
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