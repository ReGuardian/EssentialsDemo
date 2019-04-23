using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EssentialsDemo
{
    public class UnitConvertersDemo : ContentPage
    {
        private Entry entry_1;
        private Entry entry_2;
        private Entry entry_3;
        private Entry entry_4;
        private Label label_result;
        private Picker picker_converters;
        private ScrollView scrollView;

        public UnitConvertersDemo()
        {
            entry_1 = new Entry();
            entry_1.TextChanged += Entry_all_TextChanged;
            entry_2 = new Entry();
            entry_2.TextChanged += Entry_all_TextChanged;
            entry_3 = new Entry();
            entry_3.TextChanged += Entry_all_TextChanged;
            entry_4 = new Entry();
            entry_4.TextChanged += Entry_all_TextChanged;

            entry_2.IsVisible = false;
            entry_3.IsVisible = false;
            entry_4.IsVisible = false;

            label_result = new Label();

            picker_converters = new Picker();
            picker_converters.Items.Add("FahrenheitToCelsius");
            picker_converters.Items.Add("CelsiusToFahrenheit");
            picker_converters.Items.Add("CelsiusToKelvin");
            picker_converters.Items.Add("KelvinToCelsius");
            picker_converters.Items.Add("MilesToMeters");
            picker_converters.Items.Add("MilesToKilometers");
            picker_converters.Items.Add("KilometersToMiles");
            picker_converters.Items.Add("DegreesToRadians");
            picker_converters.Items.Add("RadiansToDegrees");
            picker_converters.Items.Add("DegreesPerSecondToRadiansPerSecond");
            picker_converters.Items.Add("RadiansPerSecondToDegreesPerSecond");
            picker_converters.Items.Add("DegreesPerSecondToHertz");
            picker_converters.Items.Add("RadiansPerSecondToHertz");
            picker_converters.Items.Add("HertzToDegreesPerSecond");
            picker_converters.Items.Add("HertzToRadiansPerSecond");
            picker_converters.Items.Add("KilopascalsToHectopascals");
            picker_converters.Items.Add("HectopascalsToKilopascals");
            picker_converters.Items.Add("KilopascalsToPascals");
            picker_converters.Items.Add("HectopascalsToPascals");
            picker_converters.Items.Add("AtmospheresToPascals");
            picker_converters.Items.Add("PascalsToAtmospheres");
            picker_converters.Items.Add("CoordinatesToMiles");
            picker_converters.Items.Add("CoordinatesToKilometers");

            picker_converters.SelectedIndex = 0;

            picker_converters.SelectedIndexChanged += Picker_converters_SelectedIndexChanged;

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = { new Label { Text = "This is a unit converters demo." }, entry_1, entry_2, entry_3, entry_4, picker_converters, label_result }
                }
            };

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "This is a unit converters demo." }, entry_1, entry_2, entry_3, entry_4, picker_converters, label_result
                }
            };
        }

        private void Entry_all_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entry_1.Text))
            {
                double attribute1 = double.Parse(entry_1.Text);
                switch (picker_converters.SelectedItem.ToString())
                {
                    case "FahrenheitToCelsius":
                        label_result.Text = UnitConverters.FahrenheitToCelsius(attribute1).ToString();
                        break;
                    case "CelsiusToFahrenheit":
                        label_result.Text = UnitConverters.CelsiusToFahrenheit(attribute1).ToString();
                        break;
                    case "CelsiusToKelvin":
                        label_result.Text = UnitConverters.CelsiusToKelvin(attribute1).ToString();
                        break;
                    case "KelvinToCelsius":
                        label_result.Text = UnitConverters.KelvinToCelsius(attribute1).ToString();
                        break;
                    case "MilesToMeters":
                        label_result.Text = UnitConverters.MilesToMeters(attribute1).ToString();
                        break;
                    case "MilesToKilometers":
                        label_result.Text = UnitConverters.MilesToKilometers(attribute1).ToString();
                        break;
                    case "KilometersToMiles":
                        label_result.Text = UnitConverters.KilometersToMiles(attribute1).ToString();
                        break;
                    case "DegreesToRadians":
                        label_result.Text = UnitConverters.DegreesToRadians(attribute1).ToString();
                        break;
                    case "RadiansToDegrees":
                        label_result.Text = UnitConverters.RadiansToDegrees(attribute1).ToString();
                        break;
                    case "DegreesPerSecondToRadiansPerSecond":
                        label_result.Text = UnitConverters.DegreesPerSecondToRadiansPerSecond(attribute1).ToString();
                        break;
                    case "RadiansPerSecondToDegreesPerSecond":
                        label_result.Text = UnitConverters.RadiansPerSecondToDegreesPerSecond(attribute1).ToString();
                        break;
                    case "DegreesPerSecondToHertz":
                        label_result.Text = UnitConverters.DegreesPerSecondToHertz(attribute1).ToString();
                        break;
                    case "RadiansPerSecondToHertz":
                        label_result.Text = UnitConverters.RadiansPerSecondToHertz(attribute1).ToString();
                        break;
                    case "HertzToDegreesPerSecond":
                        label_result.Text = UnitConverters.HertzToDegreesPerSecond(attribute1).ToString();
                        break;
                    case "HertzToRadiansPerSecond":
                        label_result.Text = UnitConverters.HertzToRadiansPerSecond(attribute1).ToString();
                        break;
                    case "KilopascalsToHectopascals":
                        label_result.Text = UnitConverters.KilopascalsToHectopascals(attribute1).ToString();
                        break;
                    case "HectopascalsToKilopascals":
                        label_result.Text = UnitConverters.HectopascalsToKilopascals(attribute1).ToString();
                        break;
                    case "KilopascalsToPascals":
                        label_result.Text = UnitConverters.KilopascalsToPascals(attribute1).ToString();
                        break;
                    case "HectopascalsToPascals":
                        label_result.Text = UnitConverters.HectopascalsToPascals(attribute1).ToString();
                        break;
                    case "AtmospheresToPascals":
                        label_result.Text = UnitConverters.AtmospheresToPascals(attribute1).ToString();
                        break;
                    case "PascalsToAtmospheres":
                        label_result.Text = UnitConverters.PascalsToAtmospheres(attribute1).ToString();
                        break;
                    case "CoordinatesToMiles":
                        if (!string.IsNullOrWhiteSpace(entry_2.Text) && !string.IsNullOrWhiteSpace(entry_3.Text) && !string.IsNullOrWhiteSpace(entry_4.Text))
                        {
                            double attribute2 = double.Parse(entry_2.Text);
                            double attribute3 = double.Parse(entry_3.Text);
                            double attribute4 = double.Parse(entry_4.Text);
                            label_result.Text = UnitConverters.CoordinatesToMiles(attribute1, attribute2, attribute3, attribute4).ToString();
                        }
                        break;
                    case "CoordinatesToKilometers":
                        if (!string.IsNullOrWhiteSpace(entry_2.Text) && !string.IsNullOrWhiteSpace(entry_3.Text) && !string.IsNullOrWhiteSpace(entry_4.Text))
                        {
                            double attribute2 = double.Parse(entry_2.Text);
                            double attribute3 = double.Parse(entry_3.Text);
                            double attribute4 = double.Parse(entry_4.Text);
                            label_result.Text = UnitConverters.CoordinatesToKilometers(attribute1, attribute2, attribute3, attribute4).ToString();
                        }
                        break;
                }
            }
        }

        private void Picker_converters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entry_1.Text))
            {
                entry_2.IsVisible = false;
                entry_3.IsVisible = false;
                entry_4.IsVisible = false;
                double attribute1 = double.Parse(entry_1.Text);
                switch (picker_converters.SelectedItem.ToString())
                {
                    case "FahrenheitToCelsius":
                        label_result.Text = UnitConverters.FahrenheitToCelsius(attribute1).ToString();
                        break;
                    case "CelsiusToFahrenheit":
                        label_result.Text = UnitConverters.CelsiusToFahrenheit(attribute1).ToString();
                        break;
                    case "CelsiusToKelvin":
                        label_result.Text = UnitConverters.CelsiusToKelvin(attribute1).ToString();
                        break;
                    case "KelvinToCelsius":
                        label_result.Text = UnitConverters.KelvinToCelsius(attribute1).ToString();
                        break;
                    case "MilesToMeters":
                        label_result.Text = UnitConverters.MilesToMeters(attribute1).ToString();
                        break;
                    case "MilesToKilometers":
                        label_result.Text = UnitConverters.MilesToKilometers(attribute1).ToString();
                        break;
                    case "KilometersToMiles":
                        label_result.Text = UnitConverters.KilometersToMiles(attribute1).ToString();
                        break;
                    case "DegreesToRadians":
                        label_result.Text = UnitConverters.DegreesToRadians(attribute1).ToString();
                        break;
                    case "RadiansToDegrees":
                        label_result.Text = UnitConverters.RadiansToDegrees(attribute1).ToString();
                        break;
                    case "DegreesPerSecondToRadiansPerSecond":
                        label_result.Text = UnitConverters.DegreesPerSecondToRadiansPerSecond(attribute1).ToString();
                        break;
                    case "RadiansPerSecondToDegreesPerSecond":
                        label_result.Text = UnitConverters.RadiansPerSecondToDegreesPerSecond(attribute1).ToString();
                        break;
                    case "DegreesPerSecondToHertz":
                        label_result.Text = UnitConverters.DegreesPerSecondToHertz(attribute1).ToString();
                        break;
                    case "RadiansPerSecondToHertz":
                        label_result.Text = UnitConverters.RadiansPerSecondToHertz(attribute1).ToString();
                        break;
                    case "HertzToDegreesPerSecond":
                        label_result.Text = UnitConverters.HertzToDegreesPerSecond(attribute1).ToString();
                        break;
                    case "HertzToRadiansPerSecond":
                        label_result.Text = UnitConverters.HertzToRadiansPerSecond(attribute1).ToString();
                        break;
                    case "KilopascalsToHectopascals":
                        label_result.Text = UnitConverters.KilopascalsToHectopascals(attribute1).ToString();
                        break;
                    case "HectopascalsToKilopascals":
                        label_result.Text = UnitConverters.HectopascalsToKilopascals(attribute1).ToString();
                        break;
                    case "KilopascalsToPascals":
                        label_result.Text = UnitConverters.KilopascalsToPascals(attribute1).ToString();
                        break;
                    case "HectopascalsToPascals":
                        label_result.Text = UnitConverters.HectopascalsToPascals(attribute1).ToString();
                        break;
                    case "AtmospheresToPascals":
                        label_result.Text = UnitConverters.AtmospheresToPascals(attribute1).ToString();
                        break;
                    case "PascalsToAtmospheres":
                        label_result.Text = UnitConverters.PascalsToAtmospheres(attribute1).ToString();
                        break;
                    case "CoordinatesToMiles":
                        entry_2.IsVisible = true;
                        entry_3.IsVisible = true;
                        entry_4.IsVisible = true;
                        if (!string.IsNullOrWhiteSpace(entry_2.Text) && !string.IsNullOrWhiteSpace(entry_3.Text) && !string.IsNullOrWhiteSpace(entry_4.Text))
                        {
                            double attribute2 = double.Parse(entry_2.Text);
                            double attribute3 = double.Parse(entry_3.Text);
                            double attribute4 = double.Parse(entry_4.Text);
                            label_result.Text = UnitConverters.CoordinatesToMiles(attribute1, attribute2, attribute3, attribute4).ToString();
                        }
                        break;
                    case "CoordinatesToKilometers":
                        entry_2.IsVisible = true;
                        entry_3.IsVisible = true;
                        entry_4.IsVisible = true;
                        if (!string.IsNullOrWhiteSpace(entry_2.Text) && !string.IsNullOrWhiteSpace(entry_3.Text) && !string.IsNullOrWhiteSpace(entry_4.Text))
                        {
                            double attribute2 = double.Parse(entry_2.Text);
                            double attribute3 = double.Parse(entry_3.Text);
                            double attribute4 = double.Parse(entry_4.Text);
                            label_result.Text = UnitConverters.CoordinatesToKilometers(attribute1, attribute2, attribute3, attribute4).ToString();
                        }
                        break;
                }
            }
        }
    }
}