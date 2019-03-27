using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;

namespace EssentialsDemo
{
    class GeocodingDemo : ContentPage
    {
        Button button1;
        Button button2;
        Label label;
        Entry text1;
        Entry text2;

        public GeocodingDemo()
        {
            Title = "Geocoding";

            Label header = new Label
            {
                Text = "Geocoding",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Show Address Location",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            button2 = new Button
            {
                Text = "Show App Setting Info",
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

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header, button1, button2, label
                }
            };
        }

        void OnButtonClicked1(object sender, EventArgs e)
        {
            ShowAddrLoc();//no use???
        }

        async void OnButtonClicked2(object sender, EventArgs e)
        {
            try
            {
                var lat = 47.673988;
                var lon = -122.121513;

                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    var geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    Console.WriteLine(geocodeAddress);
                    label.Text = geocodeAddress;

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }
        }

        async void ShowAddrLoc()
        {
            try
            {
                var address = "Microsoft Building 25 Redmond WA USA";
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    //label.Text = "Latitude: " + location.Latitude + "\nLongitude: " + location.Longitude + "\nAltitude: " + location.Altitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                Console.WriteLine(fnsEx);
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
                Console.WriteLine(ex);
            }
        }
    }
}