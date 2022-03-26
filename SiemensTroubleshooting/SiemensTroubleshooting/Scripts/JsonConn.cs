using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SiemensTroubleshooting.Scripts
{
    public class JsonConn
    {
        WebClient client = new WebClient();
        public class UserStaff
        {
            public string mail { get; set; }
            public string sifre { get; set; }
        }

        public class KodArama
        {
            public string[] resimurls { get; set; }
            public string[] yazilar { get; set; }
            public string kod { get; set; }
        }

        public class DataLogger
        {
            public string user_mail { get; set; }
            public string searched_error_code { get; set; }
            public string time { get; set; }
        }

        public class FeedBack
        {
            public string user_mail { get; set; }
            public bool is_solved { get; set; }
            public string feedback_text { get; set; }
            public string time { get; set; }

            public string searched_error_code { get; set; }
        }

        public class CihazBakim
        {
            public string cihaz { get; set; }
            public string[] bakim_metni { get; set; }
        }

        public class DataDetail
        {
            public string[] resimurls { get; set; }
            public string[] yazilar { get; set; }
        }

        public List<UserStaff> Login(string username)
        {

            try
            {
                var result = client.DownloadString("http://93.190.8.28:3000/users?mail=" + username + "");

                var obj = JsonConvert.DeserializeObject<List<UserStaff>>(result);


                return obj;
            }
            catch
            {
                return null;
            }
        }

        public List<KodArama> GetKodResult(string kod)
        {
            try
            {
                var result = client.DownloadString("http://93.190.8.28:3000/error_code?cod_number=" + kod + "");

                var obj = JsonConvert.DeserializeObject<List<KodArama>>(result);


                return obj;
            }
            catch
            {
                return null;
            }

        }

        public async Task LoggerAsync(string searched_error_code_M)
        {
            MainPage maildata = new MainPage();
            string mail = maildata.Mailler();
            var datalar = new DataLogger()
            {
                time = DateTime.Now.ToString("dd'-'MM'-'yyyy' - 'HH':'mm':'ss"),
                user_mail = mail,
                searched_error_code = searched_error_code_M,
            };
            var jsonString = JsonConvert.SerializeObject(datalar);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var myHttpClient = new HttpClient();
            HttpResponseMessage response = await myHttpClient.PostAsync("http://93.190.8.28:3000/user_data_logger", content);

        }

        public async Task FeedBacker(string feedback_text_M, bool is_solved_M, string searched_error_code_M)
        {
            MainPage maildata = new MainPage();
            string mail = maildata.Mailler();
            var datalar = new FeedBack()
            {
                time = DateTime.Now.ToString("dd'-'MM'-'yyyy' - 'HH':'mm':'ss"),
                user_mail = mail,
                feedback_text = feedback_text_M,
                is_solved = is_solved_M,
                searched_error_code = searched_error_code_M,
            };
            var jsonString = JsonConvert.SerializeObject(datalar);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var myHttpClient = new HttpClient();
            HttpResponseMessage response = await myHttpClient.PostAsync("http://93.190.8.28:3000/user_feedback", content);

        }

        public List<CihazBakim> GetBakimText(string cihaz_adi)
        {
            try
            {
                var result = client.DownloadString("http://93.190.8.28:3000/cihaz_bakim?cihaz=" + cihaz_adi + "");

                var obj = JsonConvert.DeserializeObject<List<CihazBakim>>(result);
                

                return obj;
            }
            catch
            {
                return null;
            }

        }
        public List<CihazBakim> KolayCozumText(string cihaz_adi)
        {
            try
            {
                var result = client.DownloadString("http://93.190.8.28:3000/kolay_cozum?cihaz=" + cihaz_adi + "");

                var obj = JsonConvert.DeserializeObject<List<CihazBakim>>(result);


                return obj;
            }
            catch
            {
                return null;
            }

        }

        public List<DataDetail> GetDetailCihazBakim(string cihaz_adi, int getselecteditemindex)
        {
            try
            {
                var result = client.DownloadString("http://93.190.8.28:3000/cihaz_bakim_icerik?cihaz=" + cihaz_adi + "&secim_id=" +getselecteditemindex);

                var obj = JsonConvert.DeserializeObject<List<DataDetail>>(result);


                return obj;
            }
            catch
            {
                return null;
            }

        }

        public List<DataDetail> ShowEasyDetail(string cihaz_adi, int getselecteditemindex)
        {
            try
            {
                var result = client.DownloadString("http://93.190.8.28:3000/kolay_cozum_icerik?cihaz=" + cihaz_adi + "&secim_id=" + getselecteditemindex);

                var obj = JsonConvert.DeserializeObject<List<DataDetail>>(result);


                return obj;
            }
            catch
            {
                return null;
            }

        }
    }
}
