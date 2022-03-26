using SiemensTroubleshooting.Scripts;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SiemensTroubleshooting.Sayfalar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecimPage : ContentPage
    {
        JsonConn json = new JsonConn();
        public SecimPage()
        {
            InitializeComponent();

            //Bakım picker nesneleri
            picker.Items.Add("Atellica CH");
            picker.Items.Add("Atellica IM");
            picker.Items.Add("Centaur XPT");
            picker.Items.Add("Chemistry XPT");
            picker.SelectedItem = "Atellica CH";

            //Kolay Çözüm picker nesneleri
            picker2.Items.Add("Atellica CH");
            picker2.Items.Add("Atellica IM");
            picker2.Items.Add("Atellica SH");
            picker2.Items.Add("Atellica SC");
            picker2.Items.Add("Centaur XPT");
            picker2.Items.Add("Chemistry XPT");
            picker2.SelectedItem = "Atellica CH";

            var yaziarray = json.KolayCozumText(picker2.SelectedItem.ToString());
            string[] asd = yaziarray[0].bakim_metni;
            var list = asd.ToList();
            liste_kolaycozum.ItemsSource = list;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new KodArama(ariza_kod_u.Text));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new KodArama("HOWTO"));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BakimPage(picker.SelectedItem.ToString()));
        }

        private void listbakimlar_ItemTapped_1(object sender, ItemTappedEventArgs e)
        {

        }

        private void picker2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var yaziarray = json.KolayCozumText(picker2.SelectedItem.ToString());
            string[] asd = yaziarray[0].bakim_metni;
            var list = asd.ToList();
            liste_kolaycozum.ItemsSource = list;
        }
    }
}