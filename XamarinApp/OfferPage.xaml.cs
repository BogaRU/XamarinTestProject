using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAppTst
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferPage : ContentPage
    {
        public string OfferJson { get; set; }
        public OfferPage(string json)   
        {
            InitializeComponent();
            OfferJson = json;
            BindingContext = this;
        }
    }
}