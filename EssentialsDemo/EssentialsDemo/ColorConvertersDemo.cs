using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace EssentialsDemo
{
    class ColorConvertersDemo : ContentPage
    {
        Label header;
        Label exception;
        Entry entry;
        Entry entry2;
        Button button;
        Button button2;
        Slider sl1, sl2, sl3, sl4;
        BoxView box;

        public ColorConvertersDemo()
        {
            Title = "Color Converter";

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
                    new RowDefinition { Height = new GridLength (20, GridUnitType.Absolute)},
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                }
            };

            header = new Label
            {
                Text = "Color Converter",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            exception = new Label
            {
                Text = "",
                TextColor = Color.Red,
                HorizontalOptions = LayoutOptions.End
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
                //Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Gray,
            };

            grid.Children.Add(header, 0, 2, 0, 1);
            grid.Children.Add(entry, 0, 1);
            grid.Children.Add(entry2, 0, 2);
            grid.Children.Add(button, 1, 1);
            grid.Children.Add(button2, 1, 2);
            grid.Children.Add(sl1, 0, 2, 3, 4);
            grid.Children.Add(sl2, 0, 2, 4, 5);
            grid.Children.Add(sl3, 0, 2, 5, 6);
            grid.Children.Add(sl4, 0, 2, 6, 7);
            grid.Children.Add(box, 0, 2, 7, 8);
            grid.Children.Add(exception, 0, 2, 8, 9);

            // Build the page.
            this.Content = grid;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                box.BackgroundColor = ColorConverters.FromHex(entry.Text);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException caught!");
                exception.Text = "Hex format is nonstandard";
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("NullReferenceException caught!");
                exception.Text = "Hex should not be null";
            }
            sl1.Value = box.BackgroundColor.Hue;
            sl2.Value = box.BackgroundColor.Saturation;
            sl3.Value = box.BackgroundColor.Luminosity;
        }

        void OnButton2Clicked(object sender, EventArgs e)
        {
            try
            {
                box.BackgroundColor = ColorConverters.FromUInt(uint.Parse(entry2.Text));
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException caught!");
                exception.Text = "UInt is not in right format";
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("NullReferenceException caught!");
                exception.Text = "UInt should not be null";
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ArgumentNullException caught!");
                exception.Text = "UInt should not be null";
            }
            sl1.Value = box.BackgroundColor.Hue;
            sl2.Value = box.BackgroundColor.Saturation;
            sl3.Value = box.BackgroundColor.Luminosity;
        }

        void Sl1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.BackgroundColor = ColorExtensions.WithHue(box.BackgroundColor, (float)sl1.Value);
            entry2.Text = ColorExtensions.ToUInt(box.BackgroundColor).ToString();
        }

        void Sl2_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.BackgroundColor = ColorExtensions.WithSaturation(box.BackgroundColor, (float)sl2.Value);
            entry2.Text = ColorExtensions.ToUInt(box.BackgroundColor).ToString();
        }

        void Sl3_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.BackgroundColor = ColorExtensions.WithLuminosity(box.BackgroundColor, (float)sl3.Value);
            entry2.Text = ColorExtensions.ToUInt(box.BackgroundColor).ToString();
        }
        void Sl4_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.BackgroundColor = ColorExtensions.WithAlpha(box.BackgroundColor, (int)sl4.Value);
            entry2.Text = ColorExtensions.ToUInt(box.BackgroundColor).ToString();
        }
    }
}