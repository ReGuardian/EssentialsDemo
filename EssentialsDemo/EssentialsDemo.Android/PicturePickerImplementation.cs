using System.IO;
using System.Threading.Tasks;
using Android.Content;
using EssentialsDemo.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PicturePickerImplementation))]

namespace EssentialsDemo.Droid
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/photo-picker
    /// Android implmentation, Accessed: 20 May 2019
    /// </summary>
    public class PicturePickerImplementation : IPicturePicker
    {
        public Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                1000);// 1000 is the MainActivity.PickImageId

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}