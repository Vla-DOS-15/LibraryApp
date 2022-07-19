using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Author
    {
        [Key]
        public int IdAuthor { get; set; }
        public string NameAuthor { get; set; }
        public ICollection<Library> Librarys { get; set; }
    }
}
