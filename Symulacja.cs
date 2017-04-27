using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulatorSamochoduProjekt
{
    class Symulacja
    {
        public decimal Pieniadze { get; set; }
        public Samochod samochod { get; set; }

        public Symulacja(Samochod samochod, decimal pieniadze)
        {
            this.Pieniadze = pieniadze;
            this.samochod = samochod;
        }
    }
}
