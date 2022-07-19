using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Genre
    {
        [Key]
        public int IdGenre { get; set; }
        public string NameGenre { get; set; }
        public ICollection<Library> Librarys { get; set; }
    }
}
