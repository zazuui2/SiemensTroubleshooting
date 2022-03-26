using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SiemensTroubleshooting.Scripts;

namespace SiemensTroubleshooting.Sayfalar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BakimPage : ContentPage
    {
        public BakimPage(string cihaz_grubu)
        {
            InitializeComponent();
            cihazbasligi.Text = cihaz_grubu + " Bakım Talimatları";
            JsonConn json = new JsonConn();
            //List<string> list = new List<string>(json.GetBakimText(cihaz_grubu)[0].bakim_metni);
            //listbakimlar.ItemsSource = list;
            var yaziarray = json.GetBakimText(cihaz_grubu);
            string[] asd = yaziarray[0].bakim_metni;
            var list = asd.ToList();
            listbakimlar.ItemsSource = list;

        }

        private async void listbakimlar_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new DetailPage(e.ItemIndex, cihazbasligi.Text, false));
            
        }
    }
}