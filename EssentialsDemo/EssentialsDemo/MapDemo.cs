using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class MapDemo : ContentPage
    {
        Label header;
        Button button1;
        Button button2;
        ScrollView scrollView;

        public MapDemo()
        {
            Title = "Map";

            header = new Label
            {
                Text = "Map",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Navigate",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1Async;

            button2 = new Button
            {
                Text = "Open Map with placemark",
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
            var location = new Location(47.645160, -122.1306032);
            var options = new MapLaunchOptions
            {
                //Name = "Microsoft Building 25" 
                NavigationMode = NavigationMode.Driving
            };

            await Map.OpenAsync(location, options);
        }

        async void OnButtonClicked2Async(object sender, EventArgs e)
        {
            var placemark = new Placemark
            {
                CountryName = "United States",
                AdminArea = "WA",
                Thoroughfare = "Microsoft Building 25",
                Locality = "Redmond"
            };
            var options = new MapLaunchOptions { Name = "Microsoft Building 25" };

            await Map.OpenAsync(placemark, options);
        }
    }
}