using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace XamarinAppTst
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            await GET();
        }

        public async Task GET()
        {
            var serializer = new XmlSerializer(typeof(YmlCatalog));
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://yastatic.net/market-export/_/partner/help/YML.xml");
                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var reader = new StreamReader(stream, Encoding.GetEncoding("Windows-1251")))
                    {
                        var ymlCatalog = (YmlCatalog)serializer.Deserialize(reader);
                        FillGrid(ymlCatalog);
                    }
                }
            }
        }

        public void FillGrid(YmlCatalog ymlCatalog)
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });

            var offerProperties = typeof(Offer).GetProperties();

            int columnIndex = 0;

            foreach (var property in offerProperties)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40) });

                var label = new Label { Text = property.Name };
                AddLabel(label, 0, columnIndex);

                int rowIndex = 1;
                foreach (var offer in ymlCatalog.Shop.Offers.OfferList)
                {
                    var value = property.GetValue(offer, null)?.ToString();

                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += async (s, e) => {
                        var json = JsonConvert.SerializeObject(offer);
                        await Navigation.PushAsync(new OfferPage(json));
                    };

                    var labelValue = new Label { Text = value, GestureRecognizers = { tapGestureRecognizer } };
                    AddLabel(labelValue, rowIndex, columnIndex);

                    rowIndex++;
                }
                columnIndex++;
            }
        }

        public void AddLabel(Label labelOfferAttribute, int row, int column)
        {
            Grid.SetRow(labelOfferAttribute, row);
            Grid.SetColumn(labelOfferAttribute, column);
            grid.Children.Add(labelOfferAttribute);
        }
        public async void OpenOfferPage(Offer offer)
        {
            string json = JsonConvert.SerializeObject(offer, Newtonsoft.Json.Formatting.Indented);
            await Navigation.PushAsync(new OfferPage(json));
        }
    }
}