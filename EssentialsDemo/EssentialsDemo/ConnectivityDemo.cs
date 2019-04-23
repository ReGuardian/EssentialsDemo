using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;

namespace EssentialsDemo
{
    class ConnectivityDemo : ContentPage
    {
        //Description of the page
        public string description = "This page shows connectivity information. " +
            "Network access has values of internet, constrained internet, local, none and unknown. " +
            "Connection profile has values of bluetooth, cellular, ethernet, wifi and unknown. ";
        Button button;
        Label label;
        Label label2;
        Label label_description;
        ScrollView scrollView;

        public ConnectivityDemo()
        {
            Title = "Connectivity";
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

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
                VerticalOptions = LayoutOptions.Center
            };

            label2 = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
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
                    Children = { header, button, label, label2, label_description }
                }
            };
            // Build the page.
            this.Content = scrollView;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            ShowConnectivityInfo();
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;
            if (access == NetworkAccess.Internet)
            {
                // Local and internet access.
                label.Text = "Network access: Internet";
            }
            else if (access == NetworkAccess.ConstrainedInternet)
            {
                // Limited internet access. 
                // Indicates captive portal connectivity, where local access to a web portal is provided, 
                // but access to the Internet requires that specific credentials are provided via a portal.
                label.Text = "Network access: ConstrainedInternett";
            }
            else if (access == NetworkAccess.Local)
            {
                // Local network access only.
                label.Text = "Network access: Local";
            }
            else if (access == NetworkAccess.None)
            {
                // No connectivity is available.
                label.Text = "Network access: None";
            }
            else if (access == NetworkAccess.Unknown)
            {
                // Unable to determine internet connectivity.
                label.Text = "Network access: Unknown";
            }

            if (access != NetworkAccess.None)
            {
                if (profiles.Contains(ConnectionProfile.Unknown))
                {
                    // Other unknown type of connection.
                    label2.Text = "Connection Profile: Unknown";
                }
                else if (profiles.Contains(ConnectionProfile.Bluetooth))
                {
                    // The bluetooth data connection.
                    label2.Text = "Connection Profile: Bluetooth";
                }
                else if (profiles.Contains(ConnectionProfile.Cellular))
                {
                    // The mobile/cellular data connection.
                    label2.Text = "Connection Profile: Celllular";
                }
                else if (profiles.Contains(ConnectionProfile.Ethernet))
                {
                    // The ethernet data connection.
                    label2.Text = "Connection Profile: Ethernet";
                }
                else if (profiles.Contains(ConnectionProfile.WiFi))
                {
                    // 	The WiFi data connection.
                    label2.Text = "Connection Profile: WiFi";
                }
            }
        }

        public void ShowConnectivityInfo()
        {
            var access = Connectivity.NetworkAccess;
            var profiles = Connectivity.ConnectionProfiles;

            if (access == NetworkAccess.Internet)
            {
                // Local and internet access.
                label.Text = "Network access: Internet";
            }
            else if (access == NetworkAccess.ConstrainedInternet)
            {
                // Limited internet access. 
                // Indicates captive portal connectivity, where local access to a web portal is provided, 
                // but access to the Internet requires that specific credentials are provided via a portal.
                label.Text = "Network access: ConstrainedInternett";
            }
            else if (access == NetworkAccess.Local)
            {
                // Local network access only.
                label.Text = "Network access: Local";
            }
            else if (access == NetworkAccess.None)
            {
                // No connectivity is available.
                label.Text = "Network access: None";
            }
            else if (access == NetworkAccess.Unknown)
            {
                // Unable to determine internet connectivity.
                label.Text = "Network access: Unknown";
            }

            if (access != NetworkAccess.None)
            {
                if (profiles.Contains(ConnectionProfile.Unknown))
                {
                    // Other unknown type of connection.
                    label2.Text = "Connection Profile: Unknown";
                }
                else if (profiles.Contains(ConnectionProfile.Bluetooth))
                {
                    // The bluetooth data connection.
                    label2.Text = "Connection Profile: Bluetooth";
                }
                else if (profiles.Contains(ConnectionProfile.Cellular))
                {
                    // The mobile/cellular data connection.
                    label2.Text = "Connection Profile: Celllular";
                }
                else if (profiles.Contains(ConnectionProfile.Ethernet))
                {
                    // The ethernet data connection.
                    label2.Text = "Connection Profile: Ethernet";
                }
                else if (profiles.Contains(ConnectionProfile.WiFi))
                {
                    // 	The WiFi data connection.
                    label2.Text = "Connection Profile: WiFi";
                }
            }
        }
    }
}