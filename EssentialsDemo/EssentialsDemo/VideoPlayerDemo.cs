using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class VideoPlayerDemo : ContentPage
    {
        VideoPlayer videoPlayer;
        Button button;

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

            this.Content = new StackLayout
            {
                Children = {videoPlayer, button }
            };
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
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
