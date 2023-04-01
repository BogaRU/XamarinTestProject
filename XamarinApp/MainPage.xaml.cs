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
        static string XmlUrl = "https://yastatic.net/market-export/_/partner/help/YML.xml";

        public MainPage()
        {
            InitializeComponent();
            LoadDataAsync(); // Вызываем метод загрузки данных при создании страницы
        }

        // Асинхронный метод загрузки данных
        public async Task LoadDataAsync()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                await GetXmlDataFromServer(); // Выполняем Get-запрос для получения данных
            }
            catch (HttpRequestException ex)
            {
                // Обработка ошибок при выполнении запроса
                await DisplayAlert("Ошибка", $"Не удалось получить данные с сервера. Текст ошибки:\n{ex.Message}", "OK");
            }
        }

        public async Task GetXmlDataFromServer()
        {
            var serializer = new XmlSerializer(typeof(YmlCatalog)); 
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(XmlUrl); // Отправляем Get-запрос
                response.EnsureSuccessStatusCode(); // Проверяем успешность выполнения запроса
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var reader = new StreamReader(stream, Encoding.GetEncoding("Windows-1251")))
                    {
                        var ymlCatalog = (YmlCatalog)serializer.Deserialize(reader); // Десериализуем данные в объект YmlCatalog
                        FillGrid(ymlCatalog); // Заполняем таблицу данными
                    }
                }
            }
        }

        // Метод заполнения таблицы данными
        public void FillGrid(YmlCatalog ymlCatalog)
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) }); // Добавляем строку заголовков

            var offerProperties = typeof(Offer).GetProperties(); // Получаем список свойств объекта Offer

            int columnIndex = 0;

            // Проходим по списку свойств и добавляем их в таблицу в виде столбцов
            foreach (var property in offerProperties)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40) });

                var label = new Label { Text = property.Name };
                AddLabel(label, 0, columnIndex);

                int rowIndex = 1;
                // Проходим по списку объектов Offer
                foreach (var offer in ymlCatalog.Shop.Offers.OfferList)
                {
                    var value = property.GetValue(offer, null)?.ToString();

                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += async (s, e) => {
                        var json = JsonConvert.SerializeObject(offer); // Сериализуем объект Offer в JSON
                        await Navigation.PushAsync(new OfferPage(json)); // Открываем страницу с детальной информацией об объекте
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
            // Сериализуем объект в формате JSON
            string json = JsonConvert.SerializeObject(offer, Newtonsoft.Json.Formatting.Indented);
            await Navigation.PushAsync(new OfferPage(json));
        }
    }
}