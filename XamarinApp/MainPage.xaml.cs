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
                    try
                    {
                        using (var reader = new StreamReader(stream, Encoding.GetEncoding("Windows-1251")))
                        {
                            var ymlCatalog = (YmlCatalog)serializer.Deserialize(reader);
                            FillGrid(ymlCatalog);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading response stream: {ex.Message}");
                    }
                }
            }
        }

        public void FillGrid(YmlCatalog ymlCatalog)
        {
            int i = 1;
            int j = 0;
            // добавляем первую строку с заголовками
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });

            var labelId = new Label { Text = "Id", HorizontalTextAlignment = TextAlignment.Start };
            Grid.SetRow(labelId, 0); // первая строка
            Grid.SetColumn(labelId, 0); // первая колонка
            grid.Children.Add(labelId);

            var labelPrice = new Label { Text = "Price", HorizontalTextAlignment = TextAlignment.End };
            Grid.SetRow(labelPrice, 0); // первая строка
            Grid.SetColumn(labelPrice, 1); // вторая колонка
            grid.Children.Add(labelPrice);

            foreach (var offer in ymlCatalog.Shop.Offers.OfferList)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

                var labelOfferId = new Label { Text = $"{offer.Id}", HorizontalTextAlignment = TextAlignment.Start };
                Grid.SetRow(labelOfferId, i);
                Grid.SetColumn(labelOfferId, j);
                grid.Children.Add(labelOfferId);

                var labelOfferPrice = new Label { Text = $"{offer.Price}", HorizontalTextAlignment = TextAlignment.End };
                Grid.SetRow(labelOfferPrice, i);
                Grid.SetColumn(labelOfferPrice, j + 1);
                grid.Children.Add(labelOfferPrice);

                i++;
            }
        }
    }
}