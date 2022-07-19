using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace LibraryApp.Gateways
{
    public class LibraryGateway
    {
        private LibraryAppContext dbContext;

        public LibraryGateway() { }
        public LibraryGateway(LibraryAppContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void LoadDB()
        {
            using (dbContext = new LibraryAppContext())
            {
                dbContext.Libraries.Load();
                dbContext.Authors.Load();
                dbContext.Genres.Load();
            }
        }
        public virtual void Read(DataGrid dataGrid)
        {
            using (dbContext = new LibraryAppContext())
            {
                //LoadDB();
                dataGrid.ItemsSource = dbContext.Libraries.Include(x=>x.Genre).Include(x=>x.Author).ToList();
            }
        }
        public virtual void Read(DataGrid dataGrid, string genre)
        {
            using (dbContext = new LibraryAppContext())
            {
                //LoadDB();
                dataGrid.ItemsSource = dbContext.Libraries.Include(x => x.Genre).Include(x => x.Author).Where(x=>x.Genre.NameGenre == genre).ToList();
            }
        }
        public string Create(string nameBook, string nameAuthor, string nameGenre, string yearCreated)
        {
            using (dbContext = new LibraryAppContext())
            {
                Author author = new Author
                {
                    NameAuthor = nameAuthor
                };
                dbContext.Authors.Add(author);
                dbContext.SaveChanges();
                Genre genre = new Genre
                {
                    NameGenre = nameGenre
                };
                //int? key = dbContext.Genres.SingleOrDefault(x => x.NameGenre == genre.NameGenre)?.IdGenre;
                //if (key == null)
                //{
                //    dbContext.Genres.Add(genre);
                //dbContext.SaveChanges();
                //}
                Library library = new Library
                {
                    NameBook = nameBook,
                    AuthorId = author.IdAuthor,
                    GenreId = genre.IdGenre,
                };
                //if (key != null)
                //{
                //    library.GenreId = (int)key;
                //    dbContext.SaveChanges();
                //}
                if (1000 <= DateTime.Now.Year)
                {
                    library.YearCreated = int.Parse(yearCreated);
                }
                else
                {
                    return "Будь ласка, введіть рік написання правильно!";
                }

                if (8 >= 0 && 8 <= 10)
                {
                    library.Rating = 8;
                    dbContext.Libraries.Add(library);
                    dbContext.SaveChanges();
                }
                else
                {
                    return "Рейтинг має бути не меньше 0 та не більше 10";
                }

                LoadDB();
                return "Дані успішно додано!";
            }
        }

        public void Update(DataGrid dataGrid)
        {
            using (dbContext = new LibraryAppContext())
            {
                int IdLib = (dataGrid.SelectedItem as Library).IdLibrary;
                int IdAut = (dataGrid.SelectedItem as Library).Author.IdAuthor;
                int IdGen = (dataGrid.SelectedItem as Library).Genre.IdGenre;

                Library updateLib = (from d in dbContext.Libraries
                                        where d.IdLibrary == IdLib
                                        select d).Single();
                Author updateAut = (from d in dbContext.Authors
                                        where d.IdAuthor == IdAut
                                        select d).Single();
                Genre updateGen = (from d in dbContext.Genres
                                    where d.IdGenre == IdGen
                                   select d).Single();

                updateLib.NameBook = (dataGrid.SelectedItem as Library).NameBook;
                updateAut.NameAuthor = (dataGrid.SelectedItem as Library).Author.NameAuthor;
                updateGen.NameGenre = (dataGrid.SelectedItem as Library).Genre.NameGenre;
                updateLib.YearCreated = (dataGrid.SelectedItem as Library).YearCreated;
                updateLib.Rating = (dataGrid.SelectedItem as Library).Rating;

                var count = dbContext.Libraries.Where(x => x.Genre.NameGenre == updateGen.NameGenre).Count();
                var maxId = dbContext.Genres.Select(x => x.IdGenre).Max();
                MessageBox.Show(count.ToString());
                if (count > 0)
                {
                    dbContext.SaveChanges();
                }
                else if (count == 0)
                {
                    Genre genre = new Genre
                    {
                        IdGenre = maxId + 1,
                        NameGenre = updateGen.NameGenre
                    };
                    dbContext.Genres.Add(genre);
                    dbContext.SaveChanges();

                    updateLib.IdLibrary = genre.IdGenre;
                    dbContext.SaveChanges();
                }
                Read(dataGrid);
            }
        }

        public void Delete(int idDel)
        {
            using (dbContext = new LibraryAppContext())
            {
                Library lib = new Library() { IdLibrary = idDel };
                dbContext.Libraries.Attach(lib);
                dbContext.Libraries.Remove(lib);
                dbContext.SaveChanges();
            }
        }
    }
}
