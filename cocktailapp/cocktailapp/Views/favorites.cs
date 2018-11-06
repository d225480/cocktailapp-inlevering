using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace cocktailapp.Views
{
    class favorites
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
      
    }
}
