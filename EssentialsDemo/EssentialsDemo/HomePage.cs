using System;
using Xamarin.Forms;

namespace EssentialsDemo
{
    public class HomePage : MasterDetailPage
    {
        AccelerometerDemo f1;
        AppInfoDemo f2;
        BarometerDemo f3;
        BatteryDemo f4;
        ClipboardDemo f5;
        CompassDemo f6;
        ConnectivityDemo f7;
        DeviceDisplayDemo f8;
        DeviceInfoDemo f9;
        EmailDemo f10;
        FileSysHelperDemo f11;
        FlashLightDemo f12;
        //GeocodingDemo f13;
        GeolocationDemo f14;
        GyroscopeDemo f15;
        LauncherDemo f16;
        MagnetometerDemo f17;
        MapDemo f19;
        BrowserDemo f20;
        OrientationSensorDemo f21;
        PhoneDialerDemo f22;
        PreferencesDemo f23;
        SecureStorageDemo f24;
        ShareDemo f25;
        SmsDemo f26;
        TextToSpeechDemo f27;
        VersionTrackingDemo f28;
        VibrationDemo f29;
        public HomePage()
        {
            Label header = new Label
            {
                Text = "Feature List",
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start
            };

            string[] pages =
            {
                "Accelerometer", "App Information", "Barometer", "Battery",
                "Clipboard", "Compass", "Connectivity", "Device Display Information",
                "Device Information", "Email", "File System Helpers", "Flashlight",
                "Geocoding", "Geolocation", "Gyroscope", "Launcher", "Magnetometer",
                "Main Thread", "Maps", "Open Browser", "Orientation Sensor", "Phone Dialer",
                "Preferences", "Secure Storage", "Share", "SMS", "Text-to-Speech",
                "Version Tracking", "Vibrate"
            };

            // Create ListView for the master page.
            ListView listView = new ListView
            {
                ItemsSource = pages
            };
            //listView.ItemSelected += ListView_ItemSelected;
            listView.ItemTapped += ListView_ItemTapped;

            f1 = new AccelerometerDemo();

            StackLayout stlaHeader = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    header
                }
            };

            // Create the master page with the ListView.
            this.Master = new ContentPage
            {
                // Title required!
                Title = "Feature List",
                Content = new StackLayout
                {
                    Children = {
                        stlaHeader,
                        listView
                    }
                }

            };
            //listView.SelectedItem = 1;
            this.Detail = f1;
            this.IsPresented = true;
            this.Title = "Demo";
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            switch (e.Item.ToString())
            {
                case "Accelerometer":
                    this.Detail = f1;
                    break;
                case "App Information":
                    if (f2 == null) { f2 = new AppInfoDemo(); }
                    this.Detail = f2;
                    break;
                case "Barometer":
                    if (f3 == null) { f3 = new BarometerDemo(); }
                    this.Detail = f3;
                    break;
                case "Battery":
                    if (f4 == null) { f4 = new BatteryDemo(); }
                    this.Detail = f4;
                    break;
                case "Clipboard":
                    if (f5 == null) { f5 = new ClipboardDemo(); }
                    this.Detail = f5;
                    break;
                case "Compass":
                    if (f6 == null) { f6 = new CompassDemo(); }
                    this.Detail = f6;
                    break;
                case "Connectivity":
                    if (f7 == null) { f7 = new ConnectivityDemo(); }
                    this.Detail = f7;
                    break;
                case "Device Display Information":
                    if (f8 == null) { f8 = new DeviceDisplayDemo(); }
                    this.Detail = f8;
                    break;
                case "Device Information":
                    if (f9 == null) { f9 = new DeviceInfoDemo(); }
                    this.Detail = f9;
                    break;
                case "Email":
                    if (f10 == null) { f10 = new EmailDemo(); }
                    this.Detail = f10;
                    break;
                case "File System Helpers":
                    if (f11 == null) { f11 = new FileSysHelperDemo(); }
                    this.Detail = f11;
                    break;
                case "Flashlight":
                    if (f12 == null) { f12 = new FlashLightDemo(); }
                    this.Detail = f12;
                    break;
                //case "Geocoding":
                //    if (f13 == null) { f13 = new GeocodingTest(); }
                //    this.Detail = f13;
                //    break;
                case "Geolocation":
                    if (f14 == null) { f14 = new GeolocationDemo(); }
                    this.Detail = f14;
                    break;
                case "Gyroscope":
                    if (f15 == null) { f15 = new GyroscopeDemo(); }
                    this.Detail = f15;
                    break;
                case "Launcher":
                    if (f16 == null) { f16 = new LauncherDemo(); }
                    this.Detail = f16;
                    break;
                case "Magnetometer":
                    if (f17 == null) { f17 = new MagnetometerDemo(); }
                    this.Detail = f17;
                    break;
                case "Maps":
                    if (f19 == null) { f19 = new MapDemo(); }
                    this.Detail = f19;
                    break;
                case "Open Browser":
                    if (f20 == null) { f20 = new BrowserDemo(); }
                    this.Detail = f20;
                    break;
                case "Orientation Sensor":
                    if (f21 == null) { f21 = new OrientationSensorDemo(); }
                    this.Detail = f21;
                    break;
                case "Phone Dialer":
                    if (f22 == null) { f22 = new PhoneDialerDemo(); }
                    this.Detail = f22;
                    break;
                case "Preferences":
                    if (f23 == null) { f23 = new PreferencesDemo(); }
                    this.Detail = f23;
                    break;
                case "Secure Storage":
                    if (f24 == null) { f24 = new SecureStorageDemo(); }
                    this.Detail = f24;
                    break;
                case "Share":
                    if (f25 == null) { f25 = new ShareDemo(); }
                    this.Detail = f25;
                    break;
                case "SMS":
                    if (f26 == null) { f26 = new SmsDemo(); }
                    this.Detail = f26;
                    break;
                case "Text-to-Speech":
                    if (f27 == null) { f27 = new TextToSpeechDemo(); }
                    this.Detail = f27;
                    break;
                case "Version Tracking":
                    if (f28 == null) { f28 = new VersionTrackingDemo(); }
                    this.Detail = f28;
                    break;
                case "Vibrate":
                    if (f29 == null) { f29 = new VibrationDemo(); }
                    this.Detail = f29;
                    break;
            }
            this.IsPresented = false;
        }
    }
}