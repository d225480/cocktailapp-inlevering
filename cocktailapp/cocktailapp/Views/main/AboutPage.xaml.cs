using cocktailapp.Views.main;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cocktailapp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
		}
        public  void settings()
        {
           
            Navigation.PushAsync(new settings());

        }
        public  void random()
        {
            string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

            var db = new SQLiteConnection(_dbpath);
            db.CreateTable<history>();
            db.CreateTable<favorites>();
            int indexer = 0;
            var table = db.Query<history>("SELECT* FROM cocktails ORDER BY RANDOM() LIMIT 1;");
            foreach (var s in table)
            {

                Navigation.PushAsync(new detailpage(s.Name));
              
            }

        }
        public void achievements()
        {
            Navigation.PushAsync(new achievements());

        }
        public  void eula()
        {
            Navigation.PushAsync(new eula());

        }

    }
}