using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace SymulatorSamochoduProjekt
{
     public class Samochod
    {
        private Timer paliwoLicznik = new Timer(100);
        public Action paliwoZmienione;
        public Action samochodOdpalony;
        public Action stanSamochoduZmieniony;

        public string nazwa { get; set; }
        public TypSamochodu rodzajSamochodu { get; set; }
        public int maxPaliwo { get; set; }
        public int paliwo { get; set; }
        public bool czyOdpalony { get; set; }
        public stanSamochodu stan { get; set; }


        public Samochod(string nazwa, TypSamochodu rodzajSamochodu, int maxPaliwo, stanSamochodu stan)
        {
            this.nazwa = nazwa;
            this.rodzajSamochodu = rodzajSamochodu;
            this.paliwo = paliwo;
            this.maxPaliwo = maxPaliwo;
            this.czyOdpalony = false;
            this.paliwoLicznik = new Timer(2000);
            this.paliwoLicznik.Elapsed += this.SkonczonePaliwo;
            this.stan = stan;
        }


        public void WlaczWylaczSamochod()
        {
            
            this.czyOdpalony = this.czyOdpalony ? false : true;

            if(this.czyOdpalony)
            {
                this.paliwoLicznik.Start();
            }
            else
            {
                this.paliwoLicznik.Stop();
            }
            if (this.samochodOdpalony != null)
            {
                this.samochodOdpalony();
            }

        }


        public void UzupelnijSamochod()
        {
            this.paliwo = this.maxPaliwo;
            if (this.paliwoZmienione != null)
            {
                this.paliwoZmienione();
            }


        }

        public void NaprawSamochod()
        {
            this.stan.Napraw(this);
            if(this.stanSamochoduZmieniony != null)
            {
                this.stanSamochoduZmieniony();
            }
        }

        public void UszkodzSamochod()
        {
            this.stan.Uszkodzenia(this);
            if(this.stanSamochoduZmieniony!= null)
            {
                this.stanSamochoduZmieniony();
            }
        }


        public int JedzSamochodem()
        {

            int km =  new Random(DateTime.Now.Second).Next(20, 101);

            if (km * 2 > this.paliwo)
            {
                int dystans = this.paliwo;
                this.paliwo = 0;
                return dystans;
            }
            else
            {
                this.paliwo -= km * 2;
                return km;
            }

        }

        public bool UszkodzeniaLosowe()
        {
            if(new Random(DateTime.Now.Second).Next(1,6)== 5)
            {
                this.UszkodzSamochod();
                return true;
            }
            else
            {
                return false;
            }
        }


        private void SkonczonePaliwo(object obj, ElapsedEventArgs e)
        {
            if(this.paliwo <= 0)
            {
                this.WlaczWylaczSamochod();
            }
            else
            {
                this.paliwo -= 1;
            }
            if(this.paliwoZmienione != null)
            {
                this.paliwoZmienione();
            }

        }


     

    }



    public static class FabrykaSamochodow
    {


        public static Samochod UtworzSamochod(TypSamochodu rodzajSamochodu, string nazwa)
        {
            int maxPaliwo = rodzajSamochodu == TypSamochodu.Sportowy ? 800
                : rodzajSamochodu == TypSamochodu.Terenowy ? 1000
                : rodzajSamochodu == TypSamochodu.Sedan ? 700
                : rodzajSamochodu == TypSamochodu.Minivan ? 200 : 0;

            stanSamochodu stan = null;

            switch(rodzajSamochodu)
            {
                case TypSamochodu.Sportowy:
                    stan = new NowyStan();
                    break;
                case TypSamochodu.Terenowy:
                    stan = new DobryStan();
                    break;
                case TypSamochodu.Sedan:
                    stan = new BardzoDobryStan();
                    break;
                case TypSamochodu.Minivan:
                    stan = new SredniStan();
                    break;

            }
            return new Samochod(nazwa, rodzajSamochodu, maxPaliwo, stan);

        }
    }
}
