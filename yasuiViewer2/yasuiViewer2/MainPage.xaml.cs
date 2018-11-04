using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace yasuiViewer2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ButtonResetUrl.Clicked += ButtonResetUrlClicked;
            ButtonFind.Clicked += ButtonFindClickedAsync;
        }

        private async void ButtonFindClickedAsync(object sender, EventArgs e)
        {
            ActivityIndicator loading = new ActivityIndicator { IsRunning = true, VerticalOptions = LayoutOptions.CenterAndExpand };
//            workArea.Content = loading;

            HttpClient client = new HttpClient();
            string urlText = Url.Text + "/yasui/getPrice?JAN=" + janCode.Text;
//            janCode.Text = urlText;
            var result = await client.GetAsync(urlText);
            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                Editor editor = new Editor { Text = content, VerticalOptions = LayoutOptions.CenterAndExpand };
                var stacks = new StackLayout();
                workArea.Content = stacks;
                
                stacks.Children.Add(editor);

                item                     i = JsonConvert.DeserializeObject<item>(content);

                Label ItemResult = getItemResult(i);
                stacks.Children.Add(ItemResult);

                Label ItemJan = getItemJan(i);
                stacks.Children.Add(ItemJan);

                Label ItemUrl = getItemUrl(i);
                stacks.Children.Add(ItemUrl);

            }
        }

        private static Label getItemResult(item i)
        {
            string s = i.RESULT;
            return  new Label() { Text = s };
        }
        private static Label getItemUrl(item i)
        {
            string s = i.URL;
            return new Label() { Text = s };
        }

        private static Label getItemJan(item i)
        {
            string s = i.JAN;
            return new Label() { Text = s };
        }


        private void ButtonResetUrlClicked(object sender, EventArgs e)
        {
            Url.Text = "https://h30jsonkadai.fivepro.xyz";
        }
    }
}
