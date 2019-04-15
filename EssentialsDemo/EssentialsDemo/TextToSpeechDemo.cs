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
        private Label title;
        private Label instructer1;
        private Slider slider_volume;
        private Label instructer2;
        private Slider slider_pitch;
        private Button button_speak;
        private Entry entry;
        private Label result;
        private Picker picker_locale;
        private Locale locale;
        private IEnumerable<Locale> locales;
        private Button button_getLocale;

        public TextToSpeechDemo()
        {
            Title = "Text-to-Speech";

            title = new Label { Text = "This is a text to speech demo." };
            entry = new Entry { Placeholder = "input here text to speech" };
            entry.Completed += Button_speak_Clicked;
            button_speak = new Button { Text = "Text to speech" };
            button_speak.Clicked += Button_speak_Clicked;
            slider_volume = new Slider { Maximum = 1.0 };
            slider_pitch = new Slider { Maximum = 2.0 };
            slider_pitch.Value = 1.0;
            slider_volume.Value = 0.75;
            instructer1 = new Label { Text = "volume" };
            instructer2 = new Label { Text = "pitch" };
            result = new Label();
            picker_locale = new Picker();


            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                button_getLocale = new Button { Text = "Get locales." };
                button_getLocale.Clicked += Button_getLocale_Clicked;


                StackLayout stloPic = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { picker_locale }
                };

                Content = new StackLayout
                {
                    Children = { title, instructer1, slider_volume, instructer2, slider_pitch, button_getLocale, stloPic, entry, button_speak, result }
                };
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
                    Children = { title, instructer1, slider_volume, instructer2, slider_pitch, stloPic, entry, button_speak, result }
                };
            }

        }

        private void Button_getLocale_Clicked(object sender, EventArgs e)
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
        }

        private void Picker_locale_SelectedIndexChanged(object sender, EventArgs e)
        {
            locale = locales.ElementAt(picker_locale.SelectedIndex);
        }

        private void Button_speak_Clicked(object sender, EventArgs e)
        {
            //SpeakNow();
            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                SpeakNow((float)slider_volume.Value, (float)slider_pitch.Value, locale, entry.Text);
            }
            else
            {
                SpeakNow((float)slider_volume.Value, (float)slider_pitch.Value, locale, "Please enter something.");
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
                //   Locale = locale ********************************************* dont know how locale works.
                Locale = locale
            };

            await TextToSpeech.SpeakAsync(text, settings);
        }

        public async void GetLocaleAsync()
        {
            locales = await TextToSpeech.GetLocalesAsync();
        }

        //public async Task SpeakNow()
        //{
        //    var settings = new SpeechOptions()
        //    {
        //        Volume = (float).75,
        //        Pitch = (float) 1.0
        //    };

        //    await TextToSpeech.SpeakAsync("Hello World", settings);
        //}

        // dont know how this cancelation works
        //public void CancelSpeech()
        //{
        //    if (cts?.IsCancellationRequested ?? false)
        //        return;

        //    cts.Cancel();
        //}
    }
}