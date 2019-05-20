﻿using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.IO;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace EssentialsDemo
{
    class ImagePickerDemo : ContentPage
    {
        public string description = "Pick button can pick image from photo library. " +
            "Take button can take a photo from camera, and show it on the screen. ";
        Label header;
        Label label_description;
        Button button;
        Button button2;
        Image image;
        StackLayout stack;
        ScrollView scrollView;

        public ImagePickerDemo()
        {
            Title = "Image Picker";

            header = new Label
            {
                Text = "Image Picker",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            label_description = new Label
            {
                Text = description,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
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

            button2 = new Button
            {
                Text = "take",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };
            button2.Clicked += OnButton2Clicked;

            stack = new StackLayout { Children = { header, button, button2, label_description } };
            scrollView = new ScrollView
            {
                Content = stack
            };

            this.Content = scrollView;
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            /*
             * https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/photo-picker
             * Implementing in Shared Code, Accessed: 20 May 2019
             */
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

                stack = new StackLayout { Children = { header, button, button2, image, label_description } };
                scrollView = new ScrollView
                {
                    Content = stack
                };
                this.Content = scrollView;
                button.IsEnabled = true;
            }
            else
            {
                button.IsEnabled = true;
            }
        }

        async void OnButton2Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            // https://github.com/jamesmontemagno/MediaPlugin
            // https://github.com/jamesmontemagno/CurrentActivityPlugin/blob/master/README.md
            // How to fully manage the permissions on Android 
            // to avoid Exceptions of no permissions of camera or storage, Accessed: 20 May 2019
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                // Codes to get access to camera and take photos
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                // Create image with the photo just took and stored stream
                image = new Image
                {
                    Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    }),
                    BackgroundColor = Color.Gray,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                // Recreate the content page
                stack = new StackLayout { Children = { header, button, button2, image, label_description } };
                scrollView = new ScrollView
                {
                    Content = stack
                };
                this.Content = scrollView;
            }
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                //On iOS you may want to send your user to the settings screen.
                //CrossPermissions.Current.OpenAppSettings();
            }
        }
    }
}