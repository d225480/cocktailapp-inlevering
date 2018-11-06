using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cocktailapp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class login : ContentPage
	{
        string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        public login ()
		{
			InitializeComponent ();
            filler();
            NavigationPage.SetHasNavigationBar(this, false);
           
        }

        private void loginn(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbpath);
            db.CreateTable<db>();

            var maxPK = db.Table<db>().OrderByDescending(c => c.Id).FirstOrDefault();


            var table = db.Query<db>("SELECT * FROM db WHERE Name = ?", username.Text);
            bool NameAvailable = true;
            foreach (var s in table)
            {
                if (s.Name == username.Text && s.password == password.Text)
                {
                    NameAvailable = false;

                    Navigation.PushAsync(new MainPage());
                }
                else
                {
                    NameAvailable = false;
                    DisplayAlert(null, "username or password was wrong ", "OK");
                }

            }
            if (NameAvailable == true)
            {
                DisplayAlert(null, "username or password was wrong ", "OK");
            }

            //   Navigation.PushAsync(new view());
        }


        private async void registerbutton(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new reigster());


        }



        //wou orgineel de excell met coktails inladen en die in de db doen heb het nu hardcoded gedaan omdat ik dat niet werkend heb gekregen en een externe database niet locaal heb kunnen laten draaien
        //zonder hem te maken in de app zelf 
       
        public static void filler()
        {
            string[] names = new string[] {
                "Draakje",
                "martini",
            "Scropino",
            "Gin and tonic"};

            string[] percentage = new string[] {
                "46,3" ,
                "50",
            "23,4",
            "6,4"};

            string[] description = new string[] {
                " shooterglas met Goldstrike ",
                "classic martini",
            "Italiaanse scropino",
            "Classic and easy, the gin and tonic is light and refreshing. It's a simple mixed drink—requiring just the two ingredients—and is perfect for happy hour, dinner, or anytime you simply want an invigorating beverage.",

            };

            string[] images = new string[] {
                "draakje.jpg",
                "level.jpg",
            "scropino.jpg",
            "gin.jpg"};

            string[] bereidingswijze = new string[] {
            "Vul een shooterglas met Goldstrike en voeg 1 druppel Pisang Ambon toe. Steek de drank in brand en zuig het geheel op met een rietje.",
            "Laat de cocktailglazen koel worden in de koelkast of diepvries.Laat de olijven uitlekken en steek een cocktailprikker schuin door de olijven. Schep de ijsblokjes in een grote mengkom of maatbeker, schenk de vermout en gin erbij en roer de cocktail een paar keer door. Verwijder de ijsblokjes met een lepel, verdeel de cocktail over de glazen en leg in elk glas een olijf.",
            "Pak een grote mengmachine of een handmixer en een kom. Doe 2/3 citroensorbetijs en 1/3 vanille-ijs in de kom, verdun dit met de prosecco totdat dit een milkshake dikte heeft.Voeg hieraan een flinke scheut vodka en ongezoete room toe zodat het geheel lekker romig wordt en mooi vermengt.Daarna nog even stevig door laten mixen en het vervolgens in een champagneglas serveren.",
            "In a highball glass filled with ice cubes, pour the gin then top it with tonic. Stir well. Garnish with a lime wedge.",
            };
            string[] glazen = new string[]
            {
                "Shooterglas",
                "martini glas",
                "scropino glas",
                "Gin and Tonic glass"

            };

        
            string[] allergieen = new string[] { };
            string[] videolinks = new string[] {
                "https://www.youtube.com/watch?v=EYdOq-DZBQM",
                "https://www.youtube.com/watch?v=1Jq4tPutdGQ"};
            for (int i = 0; i < names.Length; i++)
            {
                string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
                var db = new SQLiteConnection(_dbpath);
                db.CreateTable<cocktails>();
                var maxPK = db.Table<cocktails>().OrderByDescending(c => c.Id).FirstOrDefault();

                var table = db.Query<cocktails>("SELECT * FROM cocktails WHERE Name = ?", names[i]);
                bool NameAvailable = true;
                foreach (var s in table)
                {
                    if (s.Name == names[i])
                    {
                        NameAvailable = false;

                    }
                }
                if (NameAvailable == true)
                {

                    cocktails dbb = new cocktails()
                    {
                        Id = (maxPK == null ? 1 : maxPK.Id + 1),
                        Name = names[i],
                        description = description[i],
                        alcohol = percentage[i],
                        imagename = images[i],
                        bereidingswijze = bereidingswijze[i],
                        glas = glazen[i]


                    };
                    db.Insert(dbb);
                }

            }
         


        }
    }
}