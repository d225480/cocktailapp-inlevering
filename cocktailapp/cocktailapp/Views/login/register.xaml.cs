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
	public partial class reigster : ContentPage
	{
        string _dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        public reigster ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void registeruser(object sender, EventArgs e)
        {
            if (user.Text != null && password.Text != null && date.Date != null)
            {





                var userName = user.Text.Replace(" ", "");
                var passWord = password.Text.Replace(" ", "");
                var datePicker = date.Date;
                checkinputs(userName, passWord, datePicker);
            }
            else
            {
                DisplayAlert(null, "Een van de invulvelden was niet of fout ingevuld", "OK");


            }





        }


        


        public void checkinputs(string userName, string passWord, DateTime DatePicker)
        {

            if (userName == "" || userName.Length <= 4)
            {
                DisplayAlert(null, "gebruikersnaam was te kort of fout ingevuld", "OK");


            }
            else if (passWord != "")
            {

                if (passWord.Length <= 4)
                {
                    DisplayAlert(null, "wachtwoord was te kort moet meer dan 5 characters zijn", "OK");

                }
                else
                {
                    int age = DateTime.Today.Year - DatePicker.Date.Year;
                    if (age < 18)
                    {
                        DisplayAlert(null, "U bent nog niet oud genoeg om deze app te mogen gebruiken", "OK");


                    }
                    else
                    {


                        insert(userName, DatePicker.Date.ToString(), passWord);

                    }
                }


            }

            else if (passWord == "")
            {

                DisplayAlert(null, "Wachtwoord was leeg ", "OK");



            }

        }
        private async void insert(string name, string birthdate, string password)
        {
            var db = new SQLiteConnection(_dbpath);
            db.CreateTable<db>();

            var maxPK = db.Table<db>().OrderByDescending(c => c.Id).FirstOrDefault();


            var table = db.Query<db>("SELECT * FROM db WHERE Name = ?", name);
            bool NameAvailable = true;
            foreach (var s in table)
            {
                NameAvailable = false;

            }
            if (NameAvailable == false)
            {
                await DisplayAlert(null, "Naam is al in gebruik", "OK");
            }
            else
            {


                db dbb = new db()
                {
                    Id = (maxPK == null ? 1 : maxPK.Id + 1),
                    Name = name,
                    birthdate = birthdate,
                    password = password
                };
                db.Insert(dbb);
                Navigation.PushAsync(new MainPage());
                await DisplayAlert(null, "succesvol geregistreerd: " + name, "OK");

            }
        }
        }
}