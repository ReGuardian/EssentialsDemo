using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EssentialsDemo
{
    public class TextToSpeechDemo : ContentPage
    {
        public string description = "This function gives access to Text-to-Speech, using specified settings.";
        Label header;
        Label volume;
        Label pitch;
        Label label_description;
        Slider slider_volume;
        Slider slider_pitch;
        Entry entry;
        Picker picker_locale;
        Locale locale;
        IEnumerable<Locale> locales;
        Button button_speak;
        Button button_getLocale;
        ScrollView scrollView;

        public TextToSpeechDemo()
        {
            Title = "Text-to-Speech";

            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                        new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                        new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                        new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
                        new RowDefinition { Height = new GridLength (1, GridUnitType.Star)}
                    },
                ColumnDefinitions = {
                        new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star)},
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                    }
            };

            header = new Label
            {
                Text = "Text-to-Speech",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            slider_volume = new Slider
            {
                Maximum = 1.0,
                Value = 0.5,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                MinimumTrackColor = Color.Pink,
                MaximumTrackColor = Color.LightGray
            };

            slider_pitch = new Slider
            {
                Maximum = 2.0,
                Value = 1.0,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                MinimumTrackColor = Color.Pink,
                MaximumTrackColor = Color.LightGray
            };

            volume = new Label
            {
                Text = "Volume",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            pitch = new Label
            {
                Text = "Pitch",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            entry = new Entry
            {
                Placeholder = "Enter text",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            entry.Completed += Button_speak_Clicked;

            button_speak = new Button
            {
                Text = "Speek",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };
            button_speak.Clicked += Button_speak_Clicked;

            picker_locale = new Picker()
            {
                Title = "Locale",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            
            label_description = new Label
            {
                Text = description,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };

            grid.Children.Add(header, 0, 2, 0, 1);
            grid.Children.Add(slider_volume, 0, 1, 1, 2);
            grid.Children.Add(volume, 1, 2, 1, 2);
            grid.Children.Add(slider_pitch, 0, 1, 2, 3);
            grid.Children.Add(pitch, 1, 2, 2, 3);
            grid.Children.Add(picker_locale, 1, 2, 3, 4);
            grid.Children.Add(entry, 0, 2, 4, 5);
            grid.Children.Add(button_speak, 0, 2, 5, 6);
            grid.Children.Add(label_description, 0, 2, 6, 7);

            // Android needs a button to handle GetLocaleAsync
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                GetLocaleAsync();
                
                button_getLocale = new Button
                {
                    Text = "Get Locales",
                    Font = Font.SystemFontOfSize(NamedSize.Medium),
                    BorderWidth = 1,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    CornerRadius = 10
                };
                button_getLocale.Clicked += Button_getLocale_Clicked;

                grid.Children.Add(button_getLocale, 0, 1, 3, 4);

                scrollView = new ScrollView
                {
                    Content = grid
                };

                this.Content = scrollView;
            }
            else // Other platforms
            {
                GetLocaleAsync(); // Equal to: IEnumerable<Locale> locales = await TextToSpeech.GetLocalesAsync();

                // Grab the first locale
                locale = locales.FirstOrDefault();
                picker_locale.SelectedIndex = 0;

                foreach (Locale localeValue in locales)
                {
                    picker_locale.Items.Add(localeValue.Language);
                }
                picker_locale.SelectedIndexChanged += Picker_locale_SelectedIndexChanged;

                scrollView = new ScrollView
                {
                    Content = grid
                };

                this.Content = scrollView;
            }

        }

        private void Button_getLocale_Clicked(object sender, EventArgs e)
        {
            // Grab the first locale
            locale = locales.FirstOrDefault();
            picker_locale.SelectedIndex = 0;

            foreach (Locale localeValue in locales)
            {
                picker_locale.Items.Add(localeValue.Language);
            }
            picker_locale.SelectedIndexChanged += Picker_locale_SelectedIndexChanged;

            button_getLocale.IsEnabled = false;
        }

        private void Picker_locale_SelectedIndexChanged(object sender, EventArgs e)
        {
            locale = locales.ElementAt(picker_locale.SelectedIndex);
        }

        private async void Button_speak_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                await SpeakNow((float)slider_volume.Value, (float)slider_pitch.Value, locale, entry.Text);
            }
        }

        /// <summary>
        /// A method to speak
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task SpeakNow(float volume, float pitch, Locale locale, string text)
        {
            var settings = new SpeechOptions()
            {
                Volume = volume,
                Pitch = pitch,
                Locale = locale
            };
            await TextToSpeech.SpeakAsync(text, settings);
        }

        public async void GetLocaleAsync()
        {
            locales = await TextToSpeech.GetLocalesAsync();
        }
    }
}