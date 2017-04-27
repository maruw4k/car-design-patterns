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
using System.Windows.Shapes;

namespace SymulatorSamochoduProjekt
{
    /// <summary>
    /// Interaction logic for OknoKonfiguracyjne.xaml
    /// </summary>
    public partial class OknoKonfiguracyjne : Window
    {

        public TypSamochodu RodzajSamochodu { get; set; }
        public decimal PoczatkowePieniadze { get; set; }
        public string NazwaSamochodu { get; set; }

        public OknoKonfiguracyjne()
        {
            InitializeComponent();

            RodzajComboBox.ItemsSource = Enum.GetValues(typeof(TypSamochodu));
         
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.PoczatkowePieniadze = Decimal.Parse(this.BudzettxtBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Niepoprawna wprowadzona wartość budżetu", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.RodzajSamochodu = (TypSamochodu)RodzajComboBox.SelectedItem;
            this.NazwaSamochodu = this.NazwaSamochoduTxtBox.Text;
            DialogResult = true;
        }

        private void WyjscieBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
