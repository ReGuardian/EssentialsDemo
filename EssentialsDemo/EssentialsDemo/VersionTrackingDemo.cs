using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class VersionTrackingDemo : ContentPage
    {
        Button button1;
        Label label;
        ScrollView scrollView;
        private Label info;
        private string introduction;
        public VersionTrackingDemo()
        {
            Title = "Version Tracking";

            Label header = new Label
            {
                Text = "VersionTracking",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Show VersionTracking Info",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            VersionTracking.Track();

            label = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            introduction = "This demo gives all the information about version tracking.";
            info = new Label { Text = introduction };

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { header, button1, label, info }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        void OnButtonClicked1(object sender, EventArgs e)
        {
            VersionTracking.Track();
            label.Text = ShowVersionTrackingInfo();
        }

        public String ShowVersionTrackingInfo()
        {
            // First time ever launched application
            var firstLaunch = VersionTracking.IsFirstLaunchEver;

            // First time launching current version
            var firstLaunchCurrent = VersionTracking.IsFirstLaunchForCurrentVersion;

            // First time launching current build
            var firstLaunchBuild = VersionTracking.IsFirstLaunchForCurrentBuild;

            // Current app version (2.0.0)
            var currentVersion = VersionTracking.CurrentVersion;

            // Current build (2)
            var currentBuild = VersionTracking.CurrentBuild;

            // Previous app version (1.0.0)
            var previousVersion = VersionTracking.PreviousVersion;

            // Previous app build (1)
            var previousBuild = VersionTracking.PreviousBuild;

            // First version of app installed (1.0.0)
            var firstVersion = VersionTracking.FirstInstalledVersion;

            // First build of app installed (1)
            var firstBuild = VersionTracking.FirstInstalledBuild;

            // List of versions installed (1.0.0, 2.0.0)
            var versionHistory = "";
            foreach (string s in VersionTracking.VersionHistory)
            {
                versionHistory += (s + "; ");
            }

            // List of builds installed (1, 2)
            var buildHistory = "";
            foreach (string s in VersionTracking.BuildHistory)
            {
                buildHistory += (s + "; ");
            }

            Console.WriteLine($"Reading: First Launch: {firstLaunch}, FirstLaunchCurrent: {firstLaunchCurrent}, " +
                $"FirstLaunchBuild: {firstLaunchBuild}, Current Version: {currentVersion}, Current Build: {currentBuild}" +
                $"Previous Version: {previousVersion}, PreviousBuild: {previousBuild}, FirstVersion: {firstVersion}, " +
                $"FirstBuild: {firstBuild}, Version History: {versionHistory}, Build History: {buildHistory}");

            String info = $"First Launch: {firstLaunch}\n" +
                          $"FirstLaunchCurrent: {firstLaunchCurrent}\n" +
                          $"FirstLaunchBuild: {firstLaunchBuild}\n" +
                          $"Current Version: {currentVersion}\n" +
                          $"Current Build: {currentBuild}\n" +
                          $"Previous Version: {previousVersion}\n" +
                          $"PreviousBuild: {previousBuild}\n" +
                          $"FirstVersion: {firstVersion}\n" +
                          $"FirstBuild: {firstBuild}\n" +
                          $"Version History: {versionHistory}\n" +
                          $"Build History: {buildHistory}";
            return info;
        }
    }
}