using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EssentialsDemo
{
    public class TextToSpeechDemo : ContentPage
    {
        Label header;
        Label instructer1;
        Slider slider_volume;
        Label instructer2;
        Slider slider_pitch;
        Button button_getLocale;
        Button button_speak;
        Entry entry;
        Label result;
        Picker picker_locale;
        Locale locale;
        IEnumerable<Locale> locales;

        public TextToSpeechDemo()
        {
            Title = "Text-to-Speech";

            header = new Label
            {
                Text = "Text-To-Speech",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            instructer1 = new Label
            {
                Text = "Volume",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            instructer2 = new Label
            {
                Text = "Pitch",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            slider_volume = new Slider
            {
                Maximum = 1.0,
                Value = 0.5,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                MinimumTrackColor = Color.Pink,
                MaximumTrackColor = Color.LightGray
            };

            slider_pitch = new Slider
            {
                Maximum = 2.0,
                Value = 1.0,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                MinimumTrackColor = Color.Pink,
                MaximumTrackColor = Color.LightGray
            };

            entry = new Entry
            {
                Placeholder = "Enter text to speech",
                VerticalOptions = LayoutOptions.Center
            };
            entry.Completed += Button_speak_Clicked;

            button_speak = new Button
            {
                Text = "Speak",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 10
            };
            button_speak.Clicked += Button_speak_Clicked;

            result = new Label();

            picker_locale = new Picker();
            picker_locale.SelectedIndexChanged += Picker_locale_SelectedIndexChanged;

            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                // Android process in a different way, so need to add this line first
                GetLocaleAsync();

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
                        new RowDefinition { Height = new GridLength (1, GridUnitType.Star)}
                    },
                    ColumnDefinitions = {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                        new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star)}
                    }
                };

                button_getLocale = new Button
                {
                    Text = "Get locales",
                    Font = Font.SystemFontOfSize(NamedSize.Medium),
                    BorderWidth = 1,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    CornerRadius = 10
                };
                button_getLocale.Clicked += Button_getLocale_Clicked;

                picker_locale = new Picker()
                {
                    Title = "Locales",
                    TitleColor = Color.Gray,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center                 
                };
                
                grid.Children.Add(header, 0, 2, 0, 1);
                grid.Children.Add(instructer1, 0, 1);
                grid.Children.Add(instructer2, 0, 2);
                grid.Children.Add(slider_volume, 1, 1);
                grid.Children.Add(slider_pitch, 1, 2);
                grid.Children.Add(picker_locale, 0, 1, 3, 4);
                grid.Children.Add(button_getLocale, 1, 2, 3, 4);
                grid.Children.Add(entry, 0, 2, 4, 5);
                grid.Children.Add(button_speak, 0, 2, 5, 6);

                // Build the page.
                this.Content = grid;
            }
            else // other platforms
            {
                // equal to: IEnumerable<Locale> locales = await TextToSpeech.GetLocalesAsync(); 
                GetLocaleAsync();

                string s = "";
                foreach (Locale value in locales)
                {
                    Console.WriteLine("locale: " + value.ToString());
                    s += value.Language.ToString() + "; ";
                }
                Console.WriteLine("Triggered.");

                // Grab the first locale
                locale = locales.FirstOrDefault();
                picker_locale.SelectedIndex = 0;
                s += "FirstOrDefault: " + locales.FirstOrDefault().Language.ToString();
                result.Text = s;

                foreach (Locale localeValue in locales)
                {
                    picker_locale.Items.Add(localeValue.Language);
                }
                picker_locale.SelectedIndexChanged += Picker_locale_SelectedIndexChanged;

                StackLayout stloPic = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { picker_locale }
                };

                Content = new StackLayout
                {
                    Children = { header, instructer1, slider_volume, instructer2, slider_pitch, stloPic, entry, button_speak, result }
                };
            }
        }

        private void Button_getLocale_Clicked(object sender, EventArgs e)
        {
            // equal to: IEnumerable<Locale> locales = await TextToSpeech.GetLocalesAsync(); 
            GetLocaleAsync();

            // Grab the first locale
            locale = locales.FirstOrDefault();
            picker_locale.SelectedIndex = 0;

            foreach (Locale localeValue in locales)
            {
                picker_locale.Items.Add(localeValue.Language);
            }
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
            else
            {
                await SpeakNow((float)slider_volume.Value, (float)slider_pitch.Value, locale, "Please enter something.");
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

        // dont know how this cancelation works
        //public void CancelSpeech()
        //{
        //    if (cts?.IsCancellationRequested ?? false)
        //        return;

        //    cts.Cancel();
        //}
    }
}