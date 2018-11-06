using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cocktailapp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class eula : ContentPage
	{
		public eula ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
		}
	}
}