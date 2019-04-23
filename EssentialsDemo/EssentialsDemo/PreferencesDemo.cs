using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace EssentialsDemo
{
    //public class PreferencesDemo : ContentPage
    //   {
    //       private string content;
    //       private string key;
    //       private EntryCell entry_content;
    //       private EntryCell entry_key;
    //       private Button saveWithKey;
    //       private Button saveWithoutKey;
    //       private Button retrieveWithKey;
    //       private Button retrieveWithoutKey;
    //       private Button removeKey;
    //       private Button removeAllKeys;
    //       private Label result;
    //       public PreferencesDemo()
    //       {
    //           entry_content = new EntryCell { Label = "Content you want to save:", Placeholder = "Enter value here" };
    //           entry_key = new EntryCell { Label = "Key", Placeholder = "not neccessarily needed" };

    //           saveWithKey = new Button { Text = "Save with key" };
    //           saveWithKey.Clicked += SaveWithKey_ClickedAsync;

    //           saveWithoutKey = new Button { Text = "Save without key" };
    //           saveWithoutKey.Clicked += SaveWithoutKey_Clicked;

    //           retrieveWithKey = new Button { Text = "Retrieve with key" };
    //           retrieveWithKey.Clicked += RetrieveWithKey_Clicked;

    //           retrieveWithoutKey = new Button { Text = "Retrieve without key" };
    //           retrieveWithoutKey.Clicked += RetrieveWithoutKey_Clicked;

    //           removeKey = new Button { Text = "Remove key" };
    //           removeKey.Clicked += RemoveKey_Clicked;

    //           removeAllKeys = new Button { Text = "Remove all keys" };
    //           removeAllKeys.Clicked += RemoveAllKeys_Clicked;

    //           result = new Label();
    //           TableView tableView = new TableView
    //           {
    //               Intent = TableIntent.Form,
    //               Root = new TableRoot
    //               {
    //                   new TableSection
    //                   {
    //                       entry_content, entry_key
    //                   }
    //               }
    //           };

    //           Content = new StackLayout
    //           {
    //               Children = {
    //                   new Label { Text = "This is a Secure Stroage Demo." },tableView,saveWithKey,saveWithoutKey,retrieveWithKey,retrieveWithoutKey,removeKey,removeAllKeys,result
    //               }
    //           };
    //       }

    //       private void RemoveAllKeys_Clicked(object sender, EventArgs e)
    //       {
    //           Preferences.Clear();
    //           this.result.Text = "remove all keys: True";
    //       }

    //       private void RemoveKey_Clicked(object sender, EventArgs e)
    //       {
    //           string key = entry_key.Text;
    //           bool result = false;
    //           if (!string.IsNullOrWhiteSpace(key))
    //           {
    //               Preferences.Remove(key);
    //               this.result.Text = "remove key: " + result.ToString();
    //           }
    //           else
    //           {
    //               this.result.Text = "remove key: " + result.ToString();
    //           }
    //       }

    //       private void RetrieveWithoutKey_Clicked(object sender, EventArgs e)
    //       {
    //           try
    //           {
    //               var value = Preferences.Get(key, "The value for this key does not exist.");
    //               result.Text = value;
    //           }
    //           catch (Exception ex)
    //           {
    //               result.Text = ex.ToString();
    //           }
    //       }

    //       private void RetrieveWithKey_Clicked(object sender, EventArgs e)
    //       {
    //           string key = entry_key.Text;
    //           if (!string.IsNullOrWhiteSpace(key))
    //           {
    //               try
    //               {
    //                   var value = Preferences.Get(key, "The value for this key does not exist.");
    //                   result.Text = value;
    //               }
    //               catch (Exception ex)
    //               {
    //                   result.Text = ex.ToString();
    //               }
    //           }
    //       }

    //       private void SaveWithoutKey_Clicked(object sender, EventArgs e)
    //       {
    //           string content = this.entry_content.Text;
    //           // generate a random string.
    //           string key = Path.GetRandomFileName();
    //           key = key.Replace(".", "");

    //           if (!string.IsNullOrWhiteSpace(content) && !string.IsNullOrWhiteSpace(key))
    //           {
    //               try
    //               {
    //                   Preferences.Set(key, content);
    //                   result.Text = "Saving succeeded!";
    //               }
    //               catch (Exception ex)
    //               {
    //                   result.Text = ex.ToString();
    //               }
    //               this.content = content;
    //               this.key = key;
    //           }
    //       }

    //       private void SaveWithKey_ClickedAsync(object sender, EventArgs e)
    //       {
    //           string content = this.entry_content.Text;
    //           string key = this.entry_key.Text;

    //           if (!string.IsNullOrWhiteSpace(content) && !string.IsNullOrWhiteSpace(key))
    //           {
    //               try
    //               {
    //                   Preferences.Set(key, content);
    //                   result.Text = "Saving succeeded!";
    //               }
    //               catch (Exception ex)
    //               {
    //                   result.Text = ex.ToString();
    //               }
    //               this.content = content;
    //               this.key = key;
    //           }
    //       }
    //}
    class PreferencesDemo : ContentPage
    {
        Label header;
        Slider slider;
        Switch switcher;
        Entry text;
        DatePicker datePicker;
        ScrollView scrollView;

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

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { header, slider, switcher, text, datePicker }
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
