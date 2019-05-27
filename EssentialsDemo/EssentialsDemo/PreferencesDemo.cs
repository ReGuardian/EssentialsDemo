using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    class PreferencesDemo : ContentPage
    {
        Label header;
        Slider slider;
        Switch switcher;
        Entry text;
        DatePicker datePicker;
        ScrollView scrollView;
        private Label info;
        private string introduction;

        public PreferencesDemo()
        {
            Title = "Preferences";

            header = new Label
            {
                Text = "Preferences",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            slider = new Slider
            {
                Minimum = 0,
                Maximum = 1,
                Value = Preferences.Get("SliderValue", 0.5),
                MinimumTrackColor = Color.Pink
            };
            slider.ValueChanged += OnSliderValueChanged;

            switcher = new Switch
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsToggled = Preferences.Get("IsToggle", false)
            };
            switcher.Toggled += OnSwitcherToggled;

            text = new Entry
            {
                Keyboard = Keyboard.Text,
                Placeholder = "Enter text",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = Preferences.Get("EntryText", "")
            };
            text.TextChanged += OnEntryTextChanged;

            datePicker = new DatePicker
            {
                Format = "D",
                VerticalOptions = LayoutOptions.Start,
                Date = Preferences.Get("Date", new DateTime(2008, 6, 1))
            };
            datePicker.DateSelected += DatePicker_DateSelected;

            introduction = "This function allows the application to store preference in a kay and value pair. Once stored, next time the application is opened, the status will be set to what was stored.";
            info = new Label { Text = introduction };

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { header, slider, switcher, text, datePicker, info }
                }
            };

            // Build the page.
            this.Content = scrollView;
        }

        void OnSwitcherToggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("IsToggle", e.Value);
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            Preferences.Set("SliderValue", e.NewValue);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set("EntryText", e.NewTextValue);
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            Preferences.Set("Date", e.NewDate.Date);
        }
    }
}
