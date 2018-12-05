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
            await DisplayAlert("yasui", urlText, "OK");
            var result = await client.GetAsync(urlText);
            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                Editor editor = new Editor { Text = content, VerticalOptions = LayoutOptions.CenterAndExpand };
                var stacks = new StackLayout();
                workArea.Content = stacks;
                
                stacks.Children.Add(editor);

                item                     i = JsonConvert.DeserializeObject<item>(content);

                stacks.Children.Add(new Label { Text="Result:"});

                Label ItemResult = getItemResult(i);
                stacks.Children.Add(ItemResult);

                stacks.Children.Add(new Label { Text = "Jan:" });

                Label ItemJan = getItemJan(i);
                stacks.Children.Add(ItemJan);

                stacks.Children.Add(new Label { Text = "Product:" });

                Label ItemProduct = getItemProduct(i);
                stacks.Children.Add(ItemProduct);

                stacks.Children.Add(new Label { Text = "URL:" });

                Label ItemUrl = getItemUrl(i);
                stacks.Children.Add(ItemUrl);

                stacks.Children.Add(new Label { Text = "Price:" });

                Label ItemPrice = getItemPrice(i);
                stacks.Children.Add(ItemPrice);

            }
        }

        private static Label getItemResult(item i)
        {
            string s = i.RESULT;
            return makeLabel(s);
        }

        private static Label getItemJan(item i)
        {
            string s = i.JAN;
            return makeLabel(s);
        }
        private static Label getItemProduct(item i)
        {
            string s = i.PRODUCT;
            return makeLabel(s);
        }
        private static Label getItemUrl(item i)
        {
            string s = i.URL;
            return makeLabel(s);
        }
        private static Label getItemPrice(item i)
        {
            string s = i.PRICE;
            return makeLabel(s);
        }

        private static Label makeLabel(string s)
        {
            return new Label() { Text = s };
        }

        private void ButtonResetUrlClicked(object sender, EventArgs e)
        {
            Url.Text = "https://h30jsonkadai.fivepro.xyz";
        }
    }
}
