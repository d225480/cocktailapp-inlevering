using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cocktailapp.Views.main
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class search : ContentPage
	{
		public search (string queryname)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            results.Children.Clear();
              
                Label resultslabel = new Label();
            resultslabel.Text = "Resultaten";
               results.Children.Add(resultslabel);
            string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

            var db = new SQLiteConnection(_dbpath);
            var maxPK = db.Table<cocktails>().OrderByDescending(c => c.Id).FirstOrDefault();
            db.CreateTable<cocktails>();
            int indexer = 0;

            var table = db.Query<cocktails>(@"SELECT * FROM cocktails where name LIKE '%" + queryname + "%'");
            foreach (var s in table)
            {
                indexer++;
      
                var stack = new StackLayout
                {

                };
                string cocktailbutton = "cocktailbutton";
                var button = new Button
                {
                    HeightRequest = 60,
                    Text = s.Name

                };
                button.Clicked += detail;

                stack.Children.Add(button);
                results.Children.Add(stack);

            }





        }
        private void detail(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Navigation.PushAsync(new detailpage(btn.Text));
        }
    }
}