using cocktailapp.Views.main;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cocktailapp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class cocktailsbeck : ContentPage
	{
		public cocktailsbeck ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            getfiller();
            gethistory();
            getfavorites();
        }
        public void tmp()
        {
          
        }
        private void PressMeButton_Pressed(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Navigation.PushAsync(new detailpage(btn.Text));
        }


        public void getfiller()
        {
            suggestions.Children.Clear();
            string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

            var db = new SQLiteConnection(_dbpath);
            var maxPK = db.Table<cocktails>().OrderByDescending(c => c.Id).FirstOrDefault();
           db.CreateTable<cocktails>();
            int indexer = 0;
            var table = db.Query<cocktails>("SELECT * FROM cocktails ");
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
                button.Clicked += PressMeButton_Pressed;
            
                stack.Children.Add(button);
                suggestions.Children.Add(stack);
            
               }
           

        }
        public void getfavorites()
        {
            favorites.Children.Clear();
            string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

            var db = new SQLiteConnection(_dbpath);
            
            db.CreateTable<favorites>();
            int indexer = 0;
            var table = db.Query<favorites>("SELECT * FROM favorites ");
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
                button.Clicked += PressMeButton_Pressed;

                stack.Children.Add(button);
                favorites.Children.Add(stack);

            }
        }
        public void gethistory()
        {
            history.Children.Clear();
            string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

            var db = new SQLiteConnection(_dbpath);
            db.CreateTable<history>();
            db.CreateTable<favorites>();
            int indexer = 0;
            var table = db.Query<history>("SELECT * FROM history ");
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
                button.Clicked += PressMeButton_Pressed;

                stack.Children.Add(button);
                history.Children.Add(stack);

            }
        }

        private void search(object sender, EventArgs e)
        {
            Navigation.PushAsync(new search(searchentry.Text));
        }
    }
}