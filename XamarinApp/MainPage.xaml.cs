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
            int row = 1;

            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });

            AddLabel(new Label { Text = "Id", HorizontalTextAlignment = TextAlignment.Start }, 0, 0);
            AddLabel(new Label { Text = "Price", HorizontalTextAlignment = TextAlignment.End }, 0, 1);

            foreach (var offer in ymlCatalog.Shop.Offers.OfferList)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

                AddLabel(new Label { Text = $"{offer.Id}", HorizontalTextAlignment = TextAlignment.Start }, row, 0);
                AddLabel(new Label { Text = $"{offer.Price}", HorizontalTextAlignment = TextAlignment.End }, row, 1);
                row++;
            }
        }

        public void AddLabel(Label labelOfferAttribute, int row, int column)
        {
            Grid.SetRow(labelOfferAttribute, row);
            Grid.SetColumn(labelOfferAttribute, column);
            grid.Children.Add(labelOfferAttribute);
        }
    }
}