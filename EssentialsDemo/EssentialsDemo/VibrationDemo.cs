using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EssentialsDemo
{
    public class VibrationDemo : ContentPage
    {
        private Label title;
        private Label result;
        private Entry entry;
        private Button button_vibrarte;
        public VibrationDemo()
        {
            Title = "Vibrate";

            title = new Label { Text = "This is a vibration demo" };
            result = new Label();
            entry = new Entry { Placeholder = "Enter how many seconds to vibrate" };
            entry.Completed += Button_vibrarte_Clicked;

            button_vibrarte = new Button
            {
                Text = "Vibrate once"
            };
            button_vibrarte.Clicked += Button_vibrarte_Clicked;

            Content = new StackLayout
            {
                Children = { title, entry, button_vibrarte, result }
            };
        }
        private void Button_vibrarte_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entry.Text))
                {
                    // Use default vibration length
                    Vibration.Vibrate();
                    Console.WriteLine("Use default vibration length");
                }
                else
                {
                    // Or use specified time
                    double second = 0;
                    Double.TryParse(entry.Text, out second);
                    var duration = TimeSpan.FromSeconds(second);
                    Vibration.Vibrate(duration);
                    Console.WriteLine("Use specified time: " + duration);
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                result.Text = ex.ToString();
                Console.WriteLine(ex);
                DisplayAlert("Error", "Feature not supported on device.", "OK");
            }
            catch (Exception ex)
            {
                result.Text = ex.ToString();
                Console.WriteLine(ex);
                DisplayAlert("Error", "Other error has occurred.", "OK");
            }
        }
    }
}