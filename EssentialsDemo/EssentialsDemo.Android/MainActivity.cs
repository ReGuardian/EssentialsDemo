using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.IO;
using Android.Content;
using Plugin.Media;
using Plugin.CurrentActivity;

namespace EssentialsDemo.Droid
{
    [Activity(Label = "EssentialsDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            // Initialize for Xam.Plugin.Media
            await CrossMedia.Current.Initialize();
            // Initalize for Plugin.CurrentActivity
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Instance = this;
            LoadApplication(new App());
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            // Permisssions Request for Plugin.Media
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            // Permissions Request for Xamarin.Essentials
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // Field, property for Picture Picker
        public static readonly int PickImageId = 1000;
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        // Field, properties for Video Picker
        public static readonly int PickVideoId = 1001;
        public TaskCompletionSource<string> PickVideoTaskCompletionSource { set; get; }

        // Method for Picture Picker & Video Picker
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task(Picture Picker)
                    PickImageTaskCompletionSource.SetResult(stream);
                    // Set the filename as the completion of the Task(Video Picker)
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);// Picture Picker
                }
            }
            else if (requestCode == PickVideoId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    // Set the filename as the completion of the Task
                    PickVideoTaskCompletionSource.SetResult(intent.DataString);
                }
                else
                {
                    PickVideoTaskCompletionSource.SetResult(null);
                }
            }
        }
    }
}