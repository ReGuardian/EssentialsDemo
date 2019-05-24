using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using FormsVideoLibrary;

//https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/VideoPlayerDemos

namespace EssentialsDemo
{
    public class VideoPlayerDemo : ContentPage
    {
        VideoPlayer videoPlayer;
        Button button_selectSource;
        ScrollView scrollView;
        private Label info;
        private string introduction;
        public VideoPlayerDemo()
        {
            videoPlayer = new VideoPlayer { VerticalOptions = LayoutOptions.FillAndExpand };
            // videoPlayer.Source = VideoSource.FromUri("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4");
            // videoPlayer.Source = VideoSource.FromResource("Video/UWPApiVideo.mp4");
            // videoPlayer.Source = VideoSource.FromResource("UWPApiVideo.mp4");

            button_selectSource = new Button { Text = "Select Source" };
            button_selectSource.Clicked += Button_selectSource_ClickedAsync;

            introduction = "This demo uses the default video player of the device.";
            info = new Label { Text = introduction };

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { new Label { Text = "This is a Video Player demo." }, button_selectSource, videoPlayer, info }
                }
            };

            Content = scrollView;
        }

        private async void Button_selectSource_ClickedAsync(object sender, EventArgs e)
        {
            string filename = await DependencyService.Get<IVideoPicker>().GetVideoFileAsync();

            if (!String.IsNullOrWhiteSpace(filename))
            {
                //videoPlayer.Source = new FileVideoSource
                //{
                //    File = filename
                //};
                videoPlayer.Source = VideoSource.FromFile(filename);
            }
        }
    }
}