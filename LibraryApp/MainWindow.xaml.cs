using LibraryApp.Gateways;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LibraryGateway libraryGateway;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                libraryGateway = new LibraryGateway();
                libraryGateway.LoadDB();
                libraryGateway.Read(dataGrid);

                using (LibraryAppContext db = new LibraryAppContext())
                {
                    comboBoxSearch.ItemsSource = db.Genres.ToList();
                    comboBoxSearch.SelectedValuePath = "IdGenre";
                    comboBoxSearch.DisplayMemberPath = "NameGenre";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                libraryGateway.Update(dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                libraryGateway = new LibraryGateway();
                libraryGateway.Create(tb_NameBook.Text, tb_NameAuthor.Text, tb_Ganre.Text, tb_YearCreated.Text);
                libraryGateway.LoadDB();
                libraryGateway.Read(dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                libraryGateway = new LibraryGateway();
                libraryGateway.Delete(int.Parse(tb_IdDelete.Text));
                libraryGateway.LoadDB();
                libraryGateway.Read(dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                libraryGateway = new LibraryGateway();
                libraryGateway.LoadDB();
                libraryGateway.Read(dataGrid, comboBoxSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
