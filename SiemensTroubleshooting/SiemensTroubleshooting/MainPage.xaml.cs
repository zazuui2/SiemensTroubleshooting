using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace SiemensTroubleshooting
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            mail.Text = Preferences.Get("RandomText", string.Empty);
            sifre.Text = Preferences.Get("RandomText2", string.Empty);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Scripts.JsonConn conn = new Scripts.JsonConn();
            
            

            if (String.IsNullOrEmpty(mail.Text) || String.IsNullOrEmpty(sifre.Text))
            {
               await DisplayAlert("Uyarı", "Mail Adresiniz Veya Şifrenizi Girmediniz.", "Tamam");
            }
            else
            {
                try
                {

                    var users = conn.Login(mail.Text);
                    if (users[0].sifre == sifre.Text)
                    {
                        if (bilgilerikaydet.IsChecked)
                        {
                            Preferences.Set("RandomText", mail.Text);
                            Preferences.Set("RandomText2", sifre.Text);
                        }
                        await Navigation.PushAsync(new Sayfalar.SecimPage());
                    }
                    else
                    {
                        await DisplayAlert("Bildirim", "Girdiğiniz bilgiler yanlış", "Tamam");
                    }
                }
                catch(Exception)
                {
                    await DisplayAlert("Uyarı", "Girdiğiniz Bilgiler Yanlış ", "Tamam");
                }
                

            }
            
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new Sayfalar.FeedBack("asdas","adsasd"));
        }

        public string Mailler()
        {
            string meal = mail.Text;
            return meal;
        }
    }
}
