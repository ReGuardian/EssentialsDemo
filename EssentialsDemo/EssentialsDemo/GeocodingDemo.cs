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
        Entry entry;
        Entry entry2;
        Entry entry3;
        ScrollView scrollView;

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

            entry = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter address",
                VerticalOptions = LayoutOptions.CenterAndExpand
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

            entry2 = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter latitude",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            entry3 = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter longitude",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            button2 = new Button
            {
                Text = "Show Detail Address",
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

            scrollView = new ScrollView
            {

                Content = new StackLayout
                {
                    Children = { header, entry, button1, entry2, entry3, button2, label }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        /// <summary>
        /// Use GetLocationAsync() to show latitude, longitude and altitude
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnButtonClicked1(object sender, EventArgs e)
        {
            try
            {
                var address = entry.Text.ToString();
                var locations = await Geocoding.GetLocationsAsync(address);
                Console.WriteLine(locations.ToString());

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    label.Text = "Latitude: " + location.Latitude + "\nLongitude: " + location.Longitude + "\nAltitude: " + location.Altitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                Console.WriteLine(fnsEx);
                await DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
                Console.WriteLine(ex);
                await DisplayAlert("Error", "Other error has occurred.", "OK");
            }
        }

        /// <summary>
        /// Use latitude & longitude to create a placemark and give detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnButtonClicked2(object sender, EventArgs e)
        {
            try
            {
                var lat = Convert.ToDouble(entry2.Text);
                var lon = Convert.ToDouble(entry3.Text);

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
                Console.WriteLine(fnsEx);
                await DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
                Console.WriteLine(ex);
                await DisplayAlert("Error", "Other error has occurred.", "OK"); ;
            }
        }
    }
}