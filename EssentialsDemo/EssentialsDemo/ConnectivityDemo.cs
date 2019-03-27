using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;

namespace EssentialsDemo
{
    class ConnectivityDemo : ContentPage
    {
        Button button;
        Label label;

        public ConnectivityDemo()
        {
            Title = "Connectivity";

            Label header = new Label
            {
                Text = "Connectivity",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button = new Button
            {
                Text = "Show Connectivity Info",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };

            button.Clicked += OnButtonClicked;
            // Register for connectivity changes, be sure to unsubscribe when finished
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

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
                    header, button, label
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            label.Text = ShowConnectivityInfo();
            //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;
            Console.WriteLine($"Access: {access}, Profiles: {profiles}");
        }

        public String ShowConnectivityInfo()
        {
            var current = Connectivity.NetworkAccess;

            var profiles = Connectivity.ConnectionProfiles;

            if (current == NetworkAccess.Internet)
            {
                // Connection to internet is available
                Console.WriteLine($"Current: {current}");
            }

            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                // Active Wi-Fi connection.
                Console.WriteLine($"Profiles: {profiles}");
            }

            String info = "Current: " + current + ",\nProfiles: " + profiles;
            return info;
        }
    }
}