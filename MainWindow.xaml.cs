using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace _3333333333333
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComunicationToGus comunicationToGus = new ComunicationToGus();

            //8992689516,5261040828
            XmlSerializer serializer = new XmlSerializer(typeof(List<dane>), new XmlRootAttribute("root"));
            List<dane> result;


            if (searchInput.Text !=null && searchInput.Text.Length >= 10)
            {
                string xml = comunicationToGus.Connect(searchInput.Text);

                using (TextReader reader = new StringReader(xml))
                {
                    result = (List<dane>)serializer.Deserialize(reader);
                }
                List<dane> colection = new List<dane>();
                foreach (dane item in result)
                {
                    colection.Add(item);
                }
                dataGrid.ItemsSource = colection;
            }
            else if (searchInput.Text.Length <10)
            {
                MessageBox.Show("zbyt krotki nr nip");
                searchInput.Focus();
            }
            else if(searchInput.Text == null)
            {
                MessageBox.Show("nie znaleziono nipu w bazie danych");
                searchInput.Focus();
            }
            else
            {
                MessageBox.Show("nieznany blad");
                searchInput.Focus();
            }
        }

    }
}
