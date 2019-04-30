using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;

namespace EssentialsDemo
{
    public class SecureStorageDemo : ContentPage
    {
        private string content;
        private string key;
        private Label label_content;
        private Label label_key;
        private Label label_length;
        private Entry entry_content;
        private Entry entry_key;
        private Entry entry_length;
        private Button saveLongString;
        private Button checkLongString;
        private Button saveWithKey;
        private Button saveWithoutKey;
        private Button retrieveWithKey;
        private Button retrieveWithoutKey;
        private Button removeKey;
        private Button removeAllKeys;
        private Label result;
        private ScrollView scrollView;
        private Label info;
        private string introduction;

        public SecureStorageDemo()
        {
            Title = "Secure Storage";

            label_content = new Label { Text = "Content you want to save:" };
            entry_content = new Entry { Placeholder = "Enter value here" };

            label_key = new Label { Text = "Key:" };
            entry_key = new Entry { Placeholder = "not neccessarily needed" };

            label_length = new Label { Text = "size of string:" };
            entry_length = new Entry { Placeholder = "in kb" };

            saveLongString = new Button { Text = "Save long string" };
            saveLongString.Clicked += SaveLongString_ClickedAsync;

            checkLongString = new Button { Text = "Check long string" };
            checkLongString.Clicked += CheckLongString_ClickedAsync;

            saveWithKey = new Button { Text = "Save with key" };
            saveWithKey.Clicked += SaveWithKey_ClickedAsync;

            saveWithoutKey = new Button { Text = "Save with random key" };
            saveWithoutKey.Clicked += SaveWithoutKey_Clicked;

            retrieveWithKey = new Button { Text = "Retrieve with key" };
            retrieveWithKey.Clicked += RetrieveWithKey_Clicked;

            retrieveWithoutKey = new Button { Text = "Retrieve with random key" };
            retrieveWithoutKey.Clicked += RetrieveWithoutKey_Clicked;

            removeKey = new Button { Text = "Remove key" };
            removeKey.Clicked += RemoveKey_Clicked;

            removeAllKeys = new Button { Text = "Remove all keys" };
            removeAllKeys.Clicked += RemoveAllKeys_Clicked;

            result = new Label();
            //TableView tableView = new TableView
            //{
            //    Intent = TableIntent.Form,
            //    Root = new TableRoot
            //    {
            //        new TableSection
            //        {
            //            entry_content, entry_key, entry_length
            //        }
            //    },
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HeightRequest = 3
            //};

            introduction = "This function can be used to save simple values and key pairs. \n" +
                "The first two buttons and the length entry is used to test the length limit for this function.\n" +
                "Users can save values with specified key or with random key." +
                "Users can retrieve the values using the remaining buttons accordingly.";
            info = new Label { Text = introduction };

            scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text = "This is a Secure Stroage Demo." }, label_content, entry_content, label_key, entry_key, label_length, entry_length, saveLongString, checkLongString, saveWithKey, saveWithoutKey, retrieveWithKey, retrieveWithoutKey, removeKey, removeAllKeys, result, info
                    }
                }
            };

            Content = scrollView;
        }

        private async void CheckLongString_ClickedAsync(object sender, EventArgs e)
        {
            string key = entry_key.Text;
            if (!string.IsNullOrWhiteSpace(key))
            {
                try
                {
                    var oauthToken = await SecureStorage.GetAsync(key);
                    // result.Text = oauthToken;
                    if (oauthToken.Equals(content))
                    {
                        result.Text = "Long string retrieve succeeded";
                    }
                }
                catch (Exception ex)
                {
                    result.Text = ex.ToString();
                }
            }
        }

        private async void SaveLongString_ClickedAsync(object sender, EventArgs e)
        {
            var length_text = entry_length.Text;
            if (!string.IsNullOrWhiteSpace(length_text))
            {
                // in kb
                long length;
                length = long.Parse(length_text);
                // one char = 8 bit, 100kb = 100 * 1024 * 8 bit.
                length = length * 1024; // now in number of char.
                string longString = GenerateRandomString(length);
                content = longString;
                // save
                try
                {
                    await SecureStorage.SetAsync(this.entry_key.Text, longString);
                    result.Text = "Long string saved succeeded!";
                }
                catch (Exception ex)
                {
                    result.Text = ex.ToString();
                }
            }
        }

        private void RemoveAllKeys_Clicked(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            this.result.Text = "remove all keys: True";
        }

        private void RemoveKey_Clicked(object sender, EventArgs e)
        {
            string key = entry_key.Text;
            bool result = false;
            if (!string.IsNullOrWhiteSpace(key))
            {
                result = SecureStorage.Remove(key);
                this.result.Text = "remove key: " + result.ToString();
            }
            else
            {
                this.result.Text = "remove key: " + result.ToString();
            }
        }

        private async void RetrieveWithoutKey_Clicked(object sender, EventArgs e)
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync(key);
                result.Text = oauthToken;
            }
            catch (Exception ex)
            {
                result.Text = ex.ToString();
            }
        }

        private async void RetrieveWithKey_Clicked(object sender, EventArgs e)
        {
            string key = entry_key.Text;
            if (!string.IsNullOrWhiteSpace(key))
            {
                try
                {
                    var oauthToken = await SecureStorage.GetAsync(key);
                    result.Text = oauthToken;
                }
                catch (Exception ex)
                {
                    result.Text = ex.ToString();
                }
            }
        }

        private async void SaveWithoutKey_Clicked(object sender, EventArgs e)
        {
            string content = this.entry_content.Text;
            // generate a random string.
            string key = Path.GetRandomFileName();
            key = key.Replace(".", "");

            if (!string.IsNullOrWhiteSpace(content) && !string.IsNullOrWhiteSpace(key))
            {
                try
                {
                    await SecureStorage.SetAsync(key, content);
                    result.Text = "Saving succeeded!";
                }
                catch (Exception ex)
                {
                    result.Text = ex.ToString();
                }
                this.content = content;
                this.key = key;
            }
        }

        private async void SaveWithKey_ClickedAsync(object sender, EventArgs e)
        {
            string content = this.entry_content.Text;
            string key = this.entry_key.Text;

            if (!string.IsNullOrWhiteSpace(content) && !string.IsNullOrWhiteSpace(key))
            {
                try
                {
                    await SecureStorage.SetAsync(key, content);
                    result.Text = "Saving succeeded!";
                }
                catch (Exception ex)
                {
                    result.Text = ex.ToString();
                }
                this.content = content;
                this.key = key;
            }
        }

        private static readonly Random Random = new Random();

        private static int RandomChar => Random.Next(char.MinValue, char.MaxValue);

        private static string GenerateRandomString(long length)
        {
            var stringBuilder = new StringBuilder();

            while (stringBuilder.Length + 1 <= length)
            {
                var character = Convert.ToChar(RandomChar);
                if (!char.IsControl(character))
                {
                    // stringBuilder.Append(character);
                    stringBuilder.Append("*");
                }
            }

            return stringBuilder.ToString();
        }
    }
}