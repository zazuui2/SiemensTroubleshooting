using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FFImageLoading;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using SiemensTroubleshooting.Scripts;

namespace SiemensTroubleshooting.Sayfalar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KodArama : ContentPage
    {
        public int count = 1;
        public int maxcount;
        public string[] yazilar;
        public string[] url;
        public string pagedatavalue;
        int data_id;
        

        public KodArama(string[] parameters, bool bakimdata)
        {
            InitializeComponent();
            if (parameters.Length < 2)
            {
                pagedatavalue = parameters[0];
                OnLoad();
            }
            else if (parameters.Length > 1 && bakimdata == true)
            {
                pagedatavalue=parameters[0];
                data_id = int.Parse(parameters[1]);
                OnLoadBakim();
            }else if (parameters.Length > 1 && bakimdata == false)
            {
                pagedatavalue = parameters[0];
                data_id = int.Parse(parameters[1]);
                OnLoadEasy();
            }
            
            
            
        }

         public async void OnLoad()
        {
            Scripts.JsonConn returncodevalue = new Scripts.JsonConn();
            try
            {
                await returncodevalue.LoggerAsync(pagedatavalue);
            }
            catch (Exception ex)
            {
               await DisplayAlert("asd",ex.ToString(),"aasd");
            }
           
            try
            {
                var liste2 = returncodevalue.GetKodResult(pagedatavalue);

                
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
                    sayacsag.Text = maxcount.ToString();
                    resimItem.Source = url[0];
                    aciklama.Text = yazilar[0];
                    sayacsol.Text = count.ToString(); 
                }

            }
            catch 
            {
               
                JsonConn sender = new JsonConn();
                MainPage ana = new MainPage();
                await sender.MissingErrorCode(pagedatavalue,ana.Mailler());
               await DisplayAlert("UYARI", "Aradığınız arıza kodu sistemde kayıtlı değil arattığınız değer incelenip en kısa sürede sisteme eklencektir.", "Tamam");
               
               await Navigation.PushAsync(new Sayfalar.SecimPage());
            }

            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (count <= 1)
            {

            }
            else
            {
                ButtonIleri.Text = "İleri";
                ButtonIleri.BackgroundColor = Color.FromHex("#f26000");
                count--;

                aciklama.Text = yazilar[count - 1];
                resimItem.Source = url[count - 1];
                sayacsol.Text = count.ToString();
            }
           

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (count == maxcount)
            {
                if (ButtonIleri.Text == "Değerlendir")
                {
                    Navigation.PushAsync(new Sayfalar.FeedBack(pagedatavalue));
                }
            }
            else
            {
                if (ButtonIleri.Text == "Değerlendir")
                {
                    Navigation.PushAsync(new Sayfalar.FeedBack(pagedatavalue));
                }
                else
                {
                    if (count == (maxcount - 1) && pagedatavalue != "HOWTO")
                    {
                        count++;
                        ButtonIleri.Text = "Değerlendir";
                        ButtonIleri.BackgroundColor = Color.SteelBlue;
                        aciklama.Text = yazilar[count - 1];
                        resimItem.Source = url[count - 1];

                        sayacsol.Text = count.ToString();
                    }
                    else
                    {
                        count++;

                        aciklama.Text = yazilar[count - 1];
                        resimItem.Source = url[count - 1];

                        sayacsol.Text = count.ToString();
                    }
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new PopUP(url[count-1]));
        }

        private async void OnLoadBakim()
        {
            Scripts.JsonConn returncodevalue = new Scripts.JsonConn();
            //try
            //{
            //    await returncodevalue.LoggerAsync(pagedatavalue);
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("asd", ex.ToString(), "aasd");
            //}

            try
            {
                var liste2 = returncodevalue.GetDetailCihazBakim(pagedatavalue, data_id);


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
                    sayacsag.Text = maxcount.ToString();
                    resimItem.Source = url[0];
                    aciklama.Text = yazilar[0];
                    sayacsol.Text = count.ToString();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("UYARI", ex + "Girdiğiniz kod sistemde kayıtlı değil veya yanlış girdiniz lütfen tekrar deneyin.", "Tamam");
                await Navigation.PushAsync(new Sayfalar.SecimPage());
            }
        }

        private async void OnLoadEasy()
        {
            Scripts.JsonConn returncodevalue = new Scripts.JsonConn();
            //try
            //{
            //    await returncodevalue.LoggerAsync(pagedatavalue);
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("asd", ex.ToString(), "aasd");
            //}

            try
            {
                var liste2 = returncodevalue.ShowEasyDetail(pagedatavalue, data_id);


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
                    sayacsag.Text = maxcount.ToString();
                    resimItem.Source = url[0];
                    aciklama.Text = yazilar[0];
                    sayacsol.Text = count.ToString();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("UYARI", ex + "Girdiğiniz kod sistemde kayıtlı değil veya yanlış girdiniz lütfen tekrar deneyin.", "Tamam");
                await Navigation.PushAsync(new Sayfalar.SecimPage());
            }
        }

    }
}
