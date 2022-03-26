using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SiemensTroubleshooting.Sayfalar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedBack : ContentPage
    {
        string kod;
        public FeedBack(string ariza_kodu)
        {
            InitializeComponent();
            kod = ariza_kodu;
            
        }
        
        private void Ariza_Cozuldu_Check(object sender, CheckedChangedEventArgs e)
        {
            if (ariza_cozulmedi.IsChecked == true)
            {
                ariza_cozulmedi.IsChecked = false;
                ariza_cozuldu.IsChecked = true;
            }

        }

        private void Ariza_Cozulmedi_Check(object sender, CheckedChangedEventArgs e)
        {
            if (ariza_cozuldu.IsChecked == true)
            {
                ariza_cozuldu.IsChecked = false;
                ariza_cozulmedi.IsChecked = true;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Scripts.JsonConn json = new Scripts.JsonConn();
            if (ariza_cozuldu.IsChecked == false && ariza_cozulmedi.IsChecked == false)
            {
              await  DisplayAlert("Uyarı", "Herhangi bir seçim yapmadan geri bildirim gönderemezsiniz..", "Tamam");
            }else
            {
                if (ariza_cozuldu.IsChecked == true)
                {
                     await json.FeedBacker(gorus.Text, true, kod);
                     await DisplayAlert("", "Bildiriminiz tarafımıza ulaşmıştır. Geri dönüşünüz için teşekkürler", "Tamam");
                     await  Navigation.PushAsync(new SecimPage());
                }
                else
                {
                    await json.FeedBacker(gorus.Text, false, kod);
                    await DisplayAlert("", "Bildiriminiz tarafımıza ulaşmıştır. Geri dönüşünüz için teşekkürler", "Tamam");
                    await Navigation.PushAsync(new SecimPage());
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (ariza_cozulmedi.IsChecked == true)
            {
                ariza_cozulmedi.IsChecked = false;
                ariza_cozuldu.IsChecked = true;
            }else if (ariza_cozuldu.IsChecked == true)
            {
                ariza_cozuldu.IsChecked=false;
            }
            else
            {
                ariza_cozuldu.IsChecked = true;
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (ariza_cozuldu.IsChecked == true)
            {
                ariza_cozuldu.IsChecked = false;
                ariza_cozulmedi.IsChecked = true;
            }
            else if (ariza_cozulmedi.IsChecked == true)
            {
                ariza_cozulmedi.IsChecked = false;
            }else
            {
                ariza_cozulmedi.IsChecked = true;
            }
        }
    }
}