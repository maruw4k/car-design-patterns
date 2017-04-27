using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulatorSamochoduProjekt
{
    public abstract class stanSamochodu
    {
        public abstract void Uszkodzenia(Samochod context);
        public void Napraw(Samochod samochod)
        {
            samochod.stan = new NowyStan();
        }
    }

    public class NowyStan : stanSamochodu
    {
        public override void Uszkodzenia(Samochod samochod)
        {
            samochod.stan = new BardzoDobryStan();
        }

        public override string ToString()
        {
            return "Stan Idealny";
        }


    }



    public class BardzoDobryStan : stanSamochodu
    {
        public override void Uszkodzenia(Samochod samochod)
        {
            samochod.stan = new DobryStan();
        }

        public override string ToString()
        {
            return "Bardzo dobry";
        }


    }

    public class DobryStan : stanSamochodu
    {
        public override void Uszkodzenia(Samochod samochod)
        {
            samochod.stan = new SredniStan();
        }

        public override string ToString()
        {
            return "Dobry";
        }


    }


    public class SredniStan : stanSamochodu
    {
        public override void Uszkodzenia(Samochod samochod)
        {
            samochod.stan = new PopsutyStan();
        }

        public override string ToString()
        {
            return "Średni";
        }


    }

    public class PopsutyStan : stanSamochodu
    {
        public override void Uszkodzenia(Samochod samochod)
        {
            samochod.stan = new PopsutyStan();
        }

        public override string ToString()
        {
            return "Popsuty.";
        }


    }
}
