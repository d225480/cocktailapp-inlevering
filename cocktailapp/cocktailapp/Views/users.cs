using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cocktailapp.Views
{
    public class db
    {
        
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string birthdate { get; set; }
        public string password { get; set; }

    }
}
