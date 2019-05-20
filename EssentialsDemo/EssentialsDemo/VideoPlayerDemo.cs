using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class VideoPlayerDemo : ContentPage
    {
        public string description = "This demo uses the default video player of the device.";
        VideoPlayer videoPlayer;
        Button button;
        Label label_description;
        ScrollView scrollView;

        public VideoPlayerDemo()
        {
            Title = "Video Player";

            videoPlayer = new VideoPlayer
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            button = new Button
            {
                Text = "Show Video Library",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };
            button.Clicked += OnButtonClicked;

            label_description = new Label
            {
                Text = description,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            
            scrollView = new ScrollView
            {
                Content = new StackLayout { Children = { videoPlayer, button, label_description } }
            };
            this.Content = scrollView;
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            /*
             * https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/video-player/accessing-library
             * Invoking the dependency service, Accessed: 20 May 2019
             */
            Button btn = (Button)sender;
            btn.IsEnabled = false;

            string filename = await DependencyService.Get<IVideoPicker>().GetVideoFileAsync();

            if (!String.IsNullOrWhiteSpace(filename))
            {
                videoPlayer.Source = new FileVideoSource
                {
                    File = filename
                };
            }

            btn.IsEnabled = true;
        }
    }
}
