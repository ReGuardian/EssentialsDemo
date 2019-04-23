using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace EssentialsDemo
{
    class ColorConvertersDemo : ContentPage
    {
        // Description of the page
        public string description = "Color converters implement methods to change color with HSL value." + 
            "Clicking the first button creates color with six or eight hexadecimal numbers. " +
            "When there are eight numbers, first two numbers refer to alpha value. " +
            "Clicking the other button creates color with uint numbers. " +
            "Move the slider changes the color and corresponding uint numbers.";
        Label header;
        Label hue;
        Label saturation;
        Label luminosity;
        Label alpha;
        Entry entry;
        Entry entry2;
        Button button;
        Button button2;
        Slider sl1, sl2, sl3, sl4;
        BoxView box;
        Label label_description;
        ScrollView scrollView;

        public ColorConvertersDemo()
        {
            Title = "Color Converters";

            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength (100, GridUnitType.Absolute)},
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star)}
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                }
            };

            header = new Label
            {
                Text = "Color Converters",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            entry = new Entry
            {
                Placeholder = "Create a color by Hex"
            };

            entry2 = new Entry
            {
                Placeholder = "Create a color by UInt"
            };

            // Button to create color by Hex
            button = new Button
            {
                Text = "Create",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };
            button.Clicked += OnButtonClicked;

            // Button to create color by UInt
            button2 = new Button
            {
                Text = "Create",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };
            button2.Clicked += OnButton2Clicked;

            // Slider that change Hue
            sl1 = new Slider
            {
                Maximum = 360,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                MinimumTrackColor = Color.Pink,
                MaximumTrackColor = Color.LightGray
            };
            sl1.ValueChanged += Sl1_ValueChanged;

            // Slider that change Saturation
            sl2 = new Slider
            {
                Maximum = 100,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                MinimumTrackColor = Color.Pink,
                MaximumTrackColor = Color.LightGray
            };
            sl2.ValueChanged += Sl2_ValueChanged;

            // Slider that change Luminosity
            sl3 = new Slider
            {
                Maximum = 100,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                MinimumTrackColor = Color.Pink,
                MaximumTrackColor = Color.LightGray
            };
            sl3.ValueChanged += Sl3_ValueChanged;

            // Slider that change Alpha
            sl4 = new Slider
            {
                Maximum = 255,
                Value = 255,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                MinimumTrackColor = Color.Pink,
                MaximumTrackColor = Color.LightGray
            };
            sl4.ValueChanged += Sl4_ValueChanged;

            box = new BoxView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                Color = Color.Gray
            };

            hue = new Label
            {
                Text = "Hue",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            saturation = new Label
            {
                Text = "Saturation",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            luminosity = new Label
            {
                Text = "Luminosity",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            alpha = new Label
            {
                Text = "Alpha",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            label_description = new Label
            {
                Text = description,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };

            scrollView = new ScrollView
            {
                Content = label_description
            };

            grid.Children.Add(header, 0, 2, 0, 1);
            grid.Children.Add(entry, 0, 1);
            grid.Children.Add(entry2, 0, 2);
            grid.Children.Add(button, 1, 1);
            grid.Children.Add(button2, 1, 2);
            grid.Children.Add(sl1, 0, 1, 3, 4);
            grid.Children.Add(hue, 1, 2, 3, 4);
            grid.Children.Add(sl2, 0, 1, 4, 5);
            grid.Children.Add(saturation, 1, 2, 4, 5);
            grid.Children.Add(sl3, 0, 1, 5, 6);
            grid.Children.Add(luminosity, 1, 2, 5, 6);
            grid.Children.Add(sl4, 0, 1, 6, 7);
            grid.Children.Add(alpha, 1, 2, 6, 7);
            grid.Children.Add(box, 0, 2, 7, 8);
            grid.Children.Add(scrollView, 0, 2, 8, 9);

            // Build the page.
            this.Content = grid;
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                box.Color = ColorConverters.FromHex(entry.Text.ToString());
                Console.WriteLine("H: " + box.Color.Hue);
                Console.WriteLine("S: " + box.Color.Saturation);
                Console.WriteLine("L: " + box.Color.Luminosity);
                sl1.Value = box.Color.Hue * 360;
                sl2.Value = box.Color.Saturation * 100;
                sl3.Value = box.Color.Luminosity * 100;
                sl4.Value = box.Color.A * 255;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException caught!");
                await DisplayAlert("Error", "Hex format is nonstandard.", "OK");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("NullReferenceException caught!");
                await DisplayAlert("Error", "Hex should not be null.", "OK");
            }
        }

        void OnButton2Clicked(object sender, EventArgs e)
        {
            try
            {
                box.Color = ColorConverters.FromUInt(uint.Parse(entry2.Text));
                sl1.Value = box.Color.Hue * 360;
                sl2.Value = box.Color.Saturation * 100;
                sl3.Value = box.Color.Luminosity * 100;
                sl4.Value = box.Color.A * 255;
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException caught!");
                DisplayAlert("Error", "UInt is not in right format.", "OK");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("NullReferenceException caught!");
                DisplayAlert("Error", "UInt should not be null.", "OK");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ArgumentNullException caught!");
                DisplayAlert("Error", "UInt should not be null.", "OK");
            }
        }

        void Sl1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.Color = ColorExtensions.WithHue(box.Color, (float)sl1.Value);
            entry2.Text = ColorExtensions.ToUInt(box.Color).ToString();
        }

        void Sl2_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.Color = ColorExtensions.WithSaturation(box.Color, (float)sl2.Value);
            entry2.Text = ColorExtensions.ToUInt(box.Color).ToString();
        }

        void Sl3_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.Color = ColorExtensions.WithLuminosity(box.Color, (float)sl3.Value);
            entry2.Text = ColorExtensions.ToUInt(box.Color).ToString();
        }
        void Sl4_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.Color = ColorExtensions.WithAlpha(box.Color, (int)sl4.Value);
            entry2.Text = ColorExtensions.ToUInt(box.Color).ToString();
        }
    }
}