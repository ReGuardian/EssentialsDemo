using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;

namespace EssentialsDemo
{
    class FileSysHelperDemo : ContentPage
    {
        // Description of the page
        public string description = "Clicking first two buttons show the cache or appdata directory of the application. " +
            "Clicking the create button creates file of the file name in the first entry with content in the second entry. " +
            "It will be saved in appdata directory. It may not be found by file management app on Android " +
            "because they are hidden unless the Android device is rooted.  ";
        Label header;
        Label label;
        Button button1;
        Button button2;
        Button button3;
        Button button4;
        Entry entry;
        Entry entry2;
        Label label_description;
        ScrollView scrollView;

        public FileSysHelperDemo()
        {
            Title = "File System Helpers";

            header = new Label
            {
                Text = "File System Helper",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button1 = new Button
            {
                Text = "Cache Directory",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button1.Clicked += OnButtonClicked1;

            button2 = new Button
            {
                Text = "AppData Directory",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button2.Clicked += OnButtonClicked2;

            label = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            entry = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter file name",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            entry2 = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter text",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            button3 = new Button
            {
                Text = "Create",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button3.Clicked += OnButtonClicked3;

            button4 = new Button
            {
                Text = "Read",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button4.Clicked += OnButtonClicked4Async;

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
                    Children = { header, button1, button2, label, entry, entry2, button3, button4, label_description }
                }
            };
            // Build the page.
            this.Content = scrollView;
        }

        void OnButtonClicked1(object sender, EventArgs e)
        {
            var cacheDir = FileSystem.CacheDirectory;
            Console.WriteLine(cacheDir.ToString());
            label.Text = cacheDir.ToString();

        }

        void OnButtonClicked2(object sender, EventArgs e)
        {
            var mainDir = FileSystem.AppDataDirectory;
            Console.WriteLine(mainDir.ToString());
            label.Text = mainDir.ToString();
        }

        void OnButtonClicked3(object sender, EventArgs e)
        {
            try
            {
                var mainDir = FileSystem.AppDataDirectory;
                var fileName = Path.Combine(mainDir, entry.Text);
                File.WriteAllText(fileName, entry2.Text);
                DisplayAlert("Success", "File created successfully.", "OK");
            }
            catch (Exception)
            {
                DisplayAlert("Error", "Error has occured.", "OK");
            }
        }

        async void OnButtonClicked4Async(object sender, EventArgs e)
        {
            try
            {
                var mainDir = FileSystem.AppDataDirectory;
                var fileName = Path.Combine(mainDir, entry.Text);
                //   label.Text = File.ReadAllText(fileName);
                using (var stream = await FileSystem.OpenAppPackageFileAsync(entry.Text))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var fileContents = await reader.ReadToEndAsync();
                        label.Text = fileContents;
                    }
                }
            }
            catch (Exception)
            {
                DisplayAlert("Error", "Error has occured.", "OK");
            }
        }
    }
}