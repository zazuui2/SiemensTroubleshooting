using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
	public partial class PopUP : PopupPage
	{
		public PopUP (string resimurl)
		{
			InitializeComponent ();
			resimleer.Source = resimurl;
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
			Navigation.PopPopupAsync();
        }
    }
}