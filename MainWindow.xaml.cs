using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SymulatorSamochoduProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Symulacja symulacja;

        public MainWindow()
        {
            InitializeComponent();

            if (this.Konfiguracja())
            {
                this.Obserwator();
                this.WczytajZdjecie();
                this.OdswiezUI();
                this.UchwytStanSamochoduZmieniony();
            }
        }


        private bool Konfiguracja()
        {
            OknoKonfiguracyjne oknokonfiguracyjne = new OknoKonfiguracyjne();
            bool? rezultat = oknokonfiguracyjne.ShowDialog();
            if (rezultat == true)
            {
                Samochod nowySamochod = FabrykaSamochodow.UtworzSamochod(oknokonfiguracyjne.RodzajSamochodu, oknokonfiguracyjne.NazwaSamochodu);
                this.symulacja = new Symulacja(nowySamochod, oknokonfiguracyjne.PoczatkowePieniadze);
                return true;
            }
            else
            {
                this.Close();
                return false;
            }
        }


        private void Obserwator()
        {
            this.symulacja.samochod.paliwoZmienione = this.OdswiezUI;
            this.symulacja.samochod.samochodOdpalony = this.KontrolkiSamochodOdpalony;
            this.symulacja.samochod.stanSamochoduZmieniony = this.UchwytStanSamochoduZmieniony;
        }



        private void StartSamochoduBtn_Click(object sender, RoutedEventArgs e)
        {
            if(this.symulacja.samochod.stan.GetType() == typeof(PopsutyStan))
            {
                MessageBox.Show("Samochód jest uszkodzony i nie może ruszyć", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(this.symulacja.samochod.paliwo == 0)
            {
                MessageBox.Show("Brakuje paliwa i nie można ruszyć", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            else
            {
                this.symulacja.samochod.WlaczWylaczSamochod();
            }
        }


        private void WczytajZdjecie()
        {
            BitmapImage foto = new BitmapImage();
            foto.BeginInit();
            switch (symulacja.samochod.rodzajSamochodu.ToString())
            {
                case "Sportowy":
                    foto.UriSource = new Uri(@"Images\sportowy.jpg", UriKind.Relative);
                    break;
                case "Terenowy":
                    foto.UriSource = new Uri(@"Images\terenowy.jpg", UriKind.Relative);
                    break;
                case "Sedan":
                    foto.UriSource = new Uri(@"Images\sedan.jpg", UriKind.Relative);
                    break;
                case "Minivan":
                    foto.UriSource = new Uri(@"Images\minivan.jpg", UriKind.Relative);
                    break;
            }
            foto.EndInit();
            this.zdjecieSamochodu.Source = foto;
        }


        

        private void OdswiezUI()
        {


            Dispatcher.Invoke(new Action(() =>
            {
                this.BudzetLabel.Content = symulacja.Pieniadze.ToString() + " PLN";
                this.PaliwoLabel.Content = symulacja.samochod.paliwo.ToString();
                this.NazwaSamochoduLabel.Content = symulacja.samochod.nazwa.ToString();
            }));
        }


        private void KontrolkiSamochodOdpalony()
        {
            Dispatcher.Invoke(new Action(() => {
                this.StartSamochoduBtn.Content = this.symulacja.samochod.czyOdpalony ? "ZATRZYMAJ" : "ODPAL";
                this.JedzBtn.IsEnabled = this.symulacja.samochod.stan.GetType() != typeof(PopsutyStan) ? this.symulacja.samochod.czyOdpalony : false;
                this.lampkaSamochodowa.Fill = this.symulacja.samochod.czyOdpalony ? Brushes.Green : Brushes.Red;
                this.NaprawBtn.IsEnabled = !this.symulacja.samochod.czyOdpalony;
                this.UzupelnijPaliwoBtn.IsEnabled = !this.symulacja.samochod.czyOdpalony;
            }));
        }



        private void UchwytStanSamochoduZmieniony()
        {
            Dispatcher.Invoke(new Action(() =>
            {

                if (this.symulacja.samochod.stan.GetType() == typeof(NowyStan))
                {
                    this.StanLabel.Foreground = Brushes.Green;
                }
                else if (this.symulacja.samochod.stan.GetType() == typeof(BardzoDobryStan))
                {
                    this.StanLabel.Foreground = Brushes.DarkGreen;
                }
                else if (this.symulacja.samochod.stan.GetType() == typeof(DobryStan))
                {
                    this.StanLabel.Foreground = Brushes.LightBlue;
                }
                else if (this.symulacja.samochod.stan.GetType() == typeof(SredniStan))
                {
                    this.StanLabel.Foreground = Brushes.Red;
                }
                else if (this.symulacja.samochod.stan.GetType() == typeof(PopsutyStan))
                {
                    this.StanLabel.Foreground = Brushes.DarkGray;
                }

                this.StanLabel.Content = this.symulacja.samochod.stan.ToString();

            }));
        }

        private void JedzBtn_Click(object sender, RoutedEventArgs e)
        {
            int km = this.symulacja.samochod.JedzSamochodem();
            MessageBox.Show("Jechał " + km + "km.\n Spalił " + km * 2 + " litrów paliwa", "Powodzenie", MessageBoxButton.OK, MessageBoxImage.Information);

            if(this.symulacja.samochod.UszkodzeniaLosowe())
            {
                MessageBox.Show("Samochód został uszkodzony podczas jazdy. \n\n Napraw go zanim się zepsuję na amen.", "Samochód został uszkodzony", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void UzupelnijPaliwoBtn_Click(object sender, RoutedEventArgs e)
        {
            decimal kosztPaliwa = (this.symulacja.samochod.maxPaliwo - this.symulacja.samochod.paliwo) * 0.35m;

            if (kosztPaliwa > this.symulacja.Pieniadze)
            {
                MessageBox.Show("Nie ma wystarczającej liczby pieniędzy. \n\n Ilość możliwych litrów do wlania:" + (this.symulacja.samochod.maxPaliwo - this.symulacja.samochod.paliwo) + " Cena paliwa: " + kosztPaliwa + " PLN \nTwoje pieniądze:" + this.symulacja.Pieniadze+" PLN", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
                if (MessageBox.Show("Czy chciałbyś uzupełnić paliwo?\n\n Ilość możliwych litrów do wlania: " + (this.symulacja.samochod.maxPaliwo - this.symulacja.samochod.paliwo) + " Cena paliwa: " + kosztPaliwa+ " PLN", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                
                this.symulacja.samochod.UzupelnijSamochod();
                this.symulacja.Pieniadze -= kosztPaliwa;
                this.OdswiezUI();
                MessageBox.Show("Samochód zatankowany do pełna", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void NaprawBtn_Click(object sender, RoutedEventArgs e)
        {
            decimal kosztNaprawy = (this.symulacja.samochod.stan.GetType() == typeof(NowyStan)) ? 0.00m
                : (this.symulacja.samochod.stan.GetType() == typeof(BardzoDobryStan)) ? 10.00m
                : (this.symulacja.samochod.stan.GetType() == typeof(DobryStan)) ? 18.00m
                : (this.symulacja.samochod.stan.GetType() == typeof(SredniStan)) ? 30.00m
                : (this.symulacja.samochod.stan.GetType() == typeof(PopsutyStan)) ? 200.00m : 1000.00m;

            if(kosztNaprawy > this.symulacja.Pieniadze)
            {
                MessageBox.Show("Brak wystarczającej kwoty do naprawy.\n Kosz naprawy wynosi: $" + kosztNaprawy + "\nTwoje pieniądze: $" + this.symulacja.Pieniadze, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (MessageBox.Show("Czy na pewno chcesz naprawić samochód?\n\n Koszt naprawy: " + kosztNaprawy, "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    this.symulacja.samochod.NaprawSamochod();
                    this.symulacja.Pieniadze -= kosztNaprawy;
                    this.OdswiezUI();
                    MessageBox.Show("Samochód naprawiony", "Powodzenie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
        }
    }
}
