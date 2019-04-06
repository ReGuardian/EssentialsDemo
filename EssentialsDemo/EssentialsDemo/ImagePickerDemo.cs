using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.IO;

namespace EssentialsDemo
{
    class ImagePickerDemo : ContentPage
    {
        Label header;
        Button button;
        Image image;
        StackLayout stack;

        public ImagePickerDemo()
        {
            Title = "Image Picker";

            header = new Label
            {
                Text = "Image Picker",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            button = new Button
            {
                Text = "Pick",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };
            button.Clicked += OnButtonClicked;

            stack = new StackLayout { Children = { header, button} };

            this.Content = stack;
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            button.IsEnabled = false;
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
            
            if (stream != null)
            {
                image = new Image
                {
                    Source = ImageSource.FromStream(() => stream),
                    BackgroundColor = Color.Gray,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                TapGestureRecognizer recognizer = new TapGestureRecognizer();
                recognizer.Tapped += (sender2, args) =>
                {
                    this.Content = stack;
                    button.IsEnabled = true;
                };
                image.GestureRecognizers.Add(recognizer);

                stack = new StackLayout { Children = { header, button, image } };
                this.Content = stack;
                button.IsEnabled = true;
            }
            else
            {
                button.IsEnabled = true;
            }
        }
    }
}