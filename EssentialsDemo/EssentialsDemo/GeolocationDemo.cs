using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class GeolocationDemo : ContentPage
    {
        // Description of the page
        public string description = "Clicking the first button shows the last known location of the device latitude, longitude and altitude. " +
            "Clicking the other button shows the current location of the device in latitude, longitude and altitude. " +
            "The altitude might not be available. Both functions cannot work without access of network or gps. " +
            "Showing current location request might be slow to process, the request will be cancel after ten seconds. " +
            "So, another click is needed if there is no answer after ten seconds. ";
        Button button1;
        Button button2;
        Label label;
        Label label_description;
        ScrollView scrollView;

        public GeolocationDemo()
        {
            Title = "Geolocation";

            Label header = new Label
            {
                Text = "Geolocation",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Show last known location",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            button2 = new Button
            {
                Text = "Show current location",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button2.Clicked += OnButtonClicked2;

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
                    Children = { header, button1, button2, label, label_description }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        async void OnButtonClicked1(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    label.Text = $"Latitude:  {location.Latitude}\n" +
                                 $"Longitude: {location.Longitude}\n" +
                                 $"Altitude:  {location.Altitude}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine(fnsEx);
                await DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                Console.WriteLine(fneEx);
                await DisplayAlert("Error", "Handle not enabled on device.", "OK");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Console.WriteLine(pEx);
                await DisplayAlert("Error", "Handle permission not given.", "OK");
            }
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine(ex);
                await DisplayAlert("Error", "Unable to get location.", "OK");
            }
        }

        async void OnButtonClicked2(object sender, EventArgs e)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    label.Text = $"Latitude:  {location.Latitude}\n" +
                                 $"Longitude: {location.Longitude}\n" +
                                 $"Altitude:  {location.Altitude}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine(fnsEx);
                await DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                Console.WriteLine(fneEx);
                await DisplayAlert("Error", "Handle not enabled on device.", "OK");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Console.WriteLine(pEx);
                await DisplayAlert("Error", "Handle permission not given.", "OK");
            }
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine(ex);
                await DisplayAlert("Error", "Unable to get location.", "OK");
            }
        }
    }
}