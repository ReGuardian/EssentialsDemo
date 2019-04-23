using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class PlatformExtensionsDemo : ContentPage
    {
        Button button;
        Button button2;
        Button button3;
        Label label;
        Label label2;
        Entry entry;
        Entry entry2;
        Entry entry3;
        Entry entry4;

        public PlatformExtensionsDemo()
        {
            Title = "Platform Extensions";

            Label header = new Label
            {
                Text = "Platform Extensions",
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            entry = new Entry
            {
                Placeholder = "Integer X"
            };

            entry2 = new Entry
            {
                Placeholder = "Integer Y"
            };

            entry3 = new Entry
            {
                Placeholder = "Integer Width"
            };

            entry4 = new Entry
            {
                Placeholder = "Integer Height"
            };

            button = new Button
            {
                Text = "Get Platform Point",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button.Clicked += OnButtonClicked;

            button2 = new Button
            {
                Text = "Get Platform Size",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button2.Clicked += OnButton2Clicked;

            button3 = new Button
            {
                Text = "Get Platform Rect",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                CornerRadius = 10
            };
            button3.Clicked += OnButton3Clicked;

            label = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            label2 = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            this.Content = new StackLayout
            {
                Children = { header, entry, entry2, entry3, entry4, button, button2, button3, label, label2}
            };
        }

        /// <summary>
        /// button to get platform point from system point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var system = new System.Drawing.Point(Int32.Parse(entry.Text), Int32.Parse(entry2.Text));
                label.Text = "System: " + system.ToString();
                label2.Text = "Platform: " + DependencyService.Get<IPlatformExtensions>().GetPlatformPoint(system);
            }
            catch (Exception)
            {
                DisplayAlert("Error", "An error has occured.", "OK");
            }
        }

        /// <summary>
        /// button to get platform size from system size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnButton2Clicked(object sender, EventArgs e)
        {
            try
            {
                var system = new System.Drawing.Size(Int32.Parse(entry3.Text), Int32.Parse(entry4.Text));
                label.Text = "System: " + system.ToString();
                label2.Text = "Platform: " + DependencyService.Get<IPlatformExtensions>().GetPlatformSize(system);
            }
            catch (Exception)
            {
                DisplayAlert("Error", "An error has occured.", "OK");
            }
        }

        /// <summary>
        /// button to get platform rect from system rect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnButton3Clicked(object sender, EventArgs e)
        {
            try
            {
                var system = new System.Drawing.Rectangle(Int32.Parse(entry.Text), Int32.Parse(entry2.Text),
                        Int32.Parse(entry3.Text), Int32.Parse(entry4.Text));
                label.Text = "System: " + system.ToString();
                label2.Text = "Platform: " + DependencyService.Get<IPlatformExtensions>().GetPlatformRect(system);
            }
            catch (Exception)
            {
                DisplayAlert("Error", "An error has occured.", "OK");
            }
        }
    }
}
