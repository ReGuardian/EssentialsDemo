using System;
using System.Threading.Tasks;
using Android.Content;
using Xamarin.Forms;

// Need application's MainActivity
using EssentialsDemo.Droid;
using FormsVideoLibrary;

[assembly: Dependency(typeof(FormsVideoLibrary.Droid.VideoPicker))]

/*************************************************
* Code mainly from: 
* https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/VideoPlayerDemos
* last access: 03.06.2019
*************************************************/

namespace FormsVideoLibrary.Droid
{
    public class VideoPicker : IVideoPicker
    {
        /// <returns></returns>
        public Task<string> GetVideoFileAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("video/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            MainActivity activity = MainActivity.Instance;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Video"),
                1001); // 1001 is MainActivity.PickVideoId

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickVideoTaskCompletionSource = new TaskCompletionSource<string>();

            // Return Task object
            return activity.PickVideoTaskCompletionSource.Task;
        }
    }
}