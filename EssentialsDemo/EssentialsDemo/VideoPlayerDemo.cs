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
        public VideoPlayerDemo()
        {
            videoPlayer = new VideoPlayer();
            videoPlayer.Source = VideoSource.FromUri("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4");

            button_selectSource = new Button { Text = "Select Source" };
            button_selectSource.Clicked += Button_selectSource_ClickedAsync;

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { new Label { Text = "This is a Video Player." }, videoPlayer, button_selectSource }
                }
            };

            Content = scrollView;
        }

        private async void Button_selectSource_ClickedAsync(object sender, EventArgs e)
        {
            string filename = await DependencyService.Get<IVideoPicker>().GetVideoFileAsync();

            if (!String.IsNullOrWhiteSpace(filename))
            {
                videoPlayer.Source = new FileVideoSource
                {
                    File = filename
                };
            }
            //videoPlayer.Source = VideoSource.FromUri("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4");
        }
    }
}