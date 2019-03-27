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
        private EntryCell entry_content;
        private EntryCell entry_key;
        private Button saveWithKey;
        private Button saveWithoutKey;
        private Button retrieveWithKey;
        private Button retrieveWithoutKey;
        private Button removeKey;
        private Button removeAllKeys;
        private Label result;
        public SecureStorageDemo()
        {
            Title = "Secure Storage";

            entry_content = new EntryCell { Label = "Content you want to save:", Placeholder = "Enter value here" };
            entry_key = new EntryCell { Label = "Key", Placeholder = "not neccessarily needed" };

            saveWithKey = new Button { Text = "Save with key" };
            saveWithKey.Clicked += SaveWithKey_ClickedAsync;

            saveWithoutKey = new Button { Text = "Save without key" };
            saveWithoutKey.Clicked += SaveWithoutKey_Clicked;

            retrieveWithKey = new Button { Text = "Retrieve with key" };
            retrieveWithKey.Clicked += RetrieveWithKey_Clicked;

            retrieveWithoutKey = new Button { Text = "Retrieve without key" };
            retrieveWithoutKey.Clicked += RetrieveWithoutKey_Clicked;

            removeKey = new Button { Text = "Remove key" };
            removeKey.Clicked += RemoveKey_Clicked;

            removeAllKeys = new Button { Text = "Remove all keys" };
            removeAllKeys.Clicked += RemoveAllKeys_Clicked;

            result = new Label();
            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        entry_content, entry_key
                    }
                }
            };

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "This is a Secure Stroage Demo." },tableView,saveWithKey,saveWithoutKey,retrieveWithKey,retrieveWithoutKey,removeKey,removeAllKeys,result
                }
            };
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
    }
}