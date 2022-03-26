using Rg.Plugins.Popup.Extensions;
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
    public partial class DetailPage : ContentPage
    {

        public int count = 1;
        public int maxcount;
        public string[] yazilar;
        public string[] url;
        public int cihazid;
        public string cihazname;
        public bool gelensayfa;
        public DetailPage(int getcihazid, string getcihazname, bool anasayfa)
        {
            InitializeComponent();
            InitializeComponent();
            cihazid = getcihazid;
            cihazname = getcihazname;
            gelensayfa = anasayfa;
            OnLoad();
        }

        public async void OnLoad()
        {
            Scripts.JsonConn returncodevalue = new Scripts.JsonConn();

            if (gelensayfa)
            {
                try
                {
                    var liste2 = returncodevalue.ShowEasyDetail(cihazname,cihazid);


                    if (liste2[0].yazilar.Count() != liste2[0].resimurls.Count())
                    {
                        await DisplayAlert("Uyarı", "Arattığınız arıza kodunun değerlerinde problem olduğu için tarafınıza sunulamadı. Lütfen sorunu operasyon merkezini arayarak bildirin. Operasyon Telefon numarası: 444 0 633", "tamam");
                        await Navigation.PushAsync(new Sayfalar.SecimPage());
                    }
                    else
                    {
                        yazilar = liste2[0].yazilar;
                        url = liste2[0].resimurls;
                        maxcount = yazilar.Count();
                        sayacsag2.Text = maxcount.ToString();
                        resimItem2.Source = url[0];
                        aciklama2.Text = yazilar[0];
                        sayacsol2.Text = count.ToString();
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("UYARI", ex + "Girdiğiniz kod sistemde kayıtlı değil veya yanlış girdiniz lütfen tekrar deneyin.", "Tamam");
                    await Navigation.PushAsync(new Sayfalar.SecimPage());
                }
            }else
            {
                try
                {
                    var liste2 = returncodevalue.GetDetailCihazBakim(cihazname, cihazid);


                    if (liste2[0].yazilar.Count() != liste2[0].resimurls.Count())
                    {
                        await DisplayAlert("Uyarı", "Arattığınız arıza kodunun değerlerinde problem olduğu için tarafınıza sunulamadı. Lütfen sorunu operasyon merkezini arayarak bildirin. Operasyon Telefon numarası: 444 0 633", "tamam");
                        await Navigation.PushAsync(new Sayfalar.SecimPage());
                    }
                    else
                    {
                        yazilar = liste2[0].yazilar;
                        url = liste2[0].resimurls;
                        maxcount = yazilar.Count();
                        sayacsag2.Text = maxcount.ToString();
                        resimItem2.Source = url[0];
                        aciklama2.Text = yazilar[0];
                        sayacsol2.Text = count.ToString();
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("UYARI", ex + "Girdiğiniz kod sistemde kayıtlı değil veya yanlış girdiniz lütfen tekrar deneyin.", "Tamam");
                    await Navigation.PushAsync(new Sayfalar.SecimPage());
                }

            }



        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (count <= 1)
            {

            }
            else
            {
                ButtonIleri2.Text = "İleri";
                ButtonIleri2.BackgroundColor = Color.FromHex("#f26000");
                count--;

                aciklama2.Text = yazilar[count - 1];
                resimItem2.Source = url[count - 1];
                sayacsol2.Text = count.ToString();
            }


        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (count == maxcount)
            {
                if (ButtonIleri2.Text == "Bitir")
                {
                    Navigation.PushAsync(new Sayfalar.FeedBack(cihazname));
                }
            }
            else
            {
                if (ButtonIleri2.Text == "Değerlendir")
                {
                    Navigation.PushAsync(new Sayfalar.FeedBack(cihazname));
                }
                else
                {
                    if (count == (maxcount - 1) && cihazname != "HOWTO")
                    {
                        count++;
                        ButtonIleri2.Text = "Değerlendir";
                        ButtonIleri2.BackgroundColor = Color.SteelBlue;
                        aciklama2.Text = yazilar[count - 1];
                        resimItem2.Source = url[count - 1];

                        sayacsol2.Text = count.ToString();
                    }
                    else
                    {
                        count++;

                        aciklama2.Text = yazilar[count - 1];
                        resimItem2.Source = url[count - 1];

                        sayacsol2.Text = count.ToString();
                    }
                }
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new PopUP(url[count - 1]));
        }
    }
}