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
	public partial class detailpage : ContentPage
	{
		public detailpage ( String name)
		{
            NavigationPage.SetHasNavigationBar(this, false);
            
            InitializeComponent ();
            setvalues(name);
            addtohistory();
            checkfavorite();
		}
        public void setvalues(string name)
        {
            cocktailname.Text = name;
            string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
            var db = new SQLiteConnection(_dbpath);
            db.CreateTable<cocktails>();
            var maxPK = db.Table<cocktails>().OrderByDescending(c => c.Id).FirstOrDefault();

            var table = db.Query<cocktails>("SELECT * FROM cocktails WHERE Name = ?", name);
            bool NameAvailable = true;
            foreach (var s in table)
            {
                if (s.Name == name)
                {
                    NameAvailable = false;
                    image.Source = s.imagename;
                    description.Text = description.Text + s.description;
                    alcohol.Text = alcohol.Text + s.alcohol;
                    bereidingswijze.Text = bereidingswijze.Text + s.bereidingswijze;
                    glas.Text = glas.Text + s.glas;


                }
            }
            if (NameAvailable == true)
            {

             
            }

        }
     

   

    public void addtohistory()
        {
         
                string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
                var db = new SQLiteConnection(_dbpath);
               

                var maxPK = db.Table<cocktails>().OrderByDescending(c => c.Id).FirstOrDefault();
                var table = db.Query<cocktails>("SELECT * FROM cocktails WHERE Name = ?", cocktailname.Text );

                var dbhistory = new SQLiteConnection(_dbpath);
                dbhistory.CreateTable<history>();
                var maxPKhisotry = dbhistory.Table<history>().OrderByDescending(c => c.Id).FirstOrDefault();
                var tablehistory = dbhistory.Query<history>("SELECT * FROM history WHERE Name = ?", cocktailname.Text);



            bool NameAvailable = true;
                foreach (var s in table)
                {
                    if (s.Name == cocktailname.Text)
                    {
                        foreach (var p in tablehistory)
                        {
                        int id = p.Id;

                        dbhistory.Delete(p);
                         }

                        history dbb = new history()
                            {
                                Id = (maxPKhisotry == null ? 1 : maxPKhisotry.Id + 1),
                                Name = cocktailname.Text,
                                description = description.Text,
                                alcohol = alcohol.Text,
                                imagename = image.Source.ToString(),
                                

                            };
                        dbhistory.Insert(dbb);

                    }
                }
               

           



        }
        public void checkfavorite()
        {
          
            string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

            var db = new SQLiteConnection(_dbpath);
            int indexer = 0;
            var table = db.Query<favorites>("SELECT * FROM favorites where name = ? ", cocktailname.Text);
            foreach (var s in table)
            {
                favorites.Clicked += removefavorite;
                favorites.Text = "Weghalen uit favorieten";
            }
        }
        private void removefavorite(object sender, EventArgs e)
        {
            string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

            var db = new SQLiteConnection(_dbpath);
            int indexer = 0;
            var table = db.Query<favorites>("SELECT * FROM favorites where name = ? ", cocktailname.Text);
            foreach (var s in table)
            {
                db.Delete(s);
            }
            favorites.Clicked += addfavorite;
            favorites.Text = "Toevoegen aan favorieten";
            checkfavorite();
        }

        private void addfavorite(object sender, EventArgs e)
        {
            string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

            var dbhistory = new SQLiteConnection(_dbpath);
            dbhistory.CreateTable<favorites>();
            var maxPKhisotry = dbhistory.Table<favorites>().OrderByDescending(c => c.Id).FirstOrDefault();
            var tablehistory = dbhistory.Query<favorites>("SELECT * FROM history WHERE Name = ?", cocktailname.Text);

            favorites dbb = new favorites()
            {
                Id = (maxPKhisotry == null ? 1 : maxPKhisotry.Id + 1),
                Name = cocktailname.Text,
            };
            dbhistory.Insert(dbb);
            checkfavorite();
        }


    }

}