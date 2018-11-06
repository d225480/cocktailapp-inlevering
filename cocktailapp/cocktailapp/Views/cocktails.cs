using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace cocktailapp.Views
{
    class cocktails
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public string alcohol { get; set; }
        public string imagename { get; set; }
        public string bereidingswijze { get; set; }
        public string ingredienten { get; set; }
        public string allergieen { get; set; }
        public string video { get; set; }
        public string glas { get; set; }

    }
}
