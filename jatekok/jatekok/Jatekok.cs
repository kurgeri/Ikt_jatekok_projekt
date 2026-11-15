using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jatekok
{
    public class Jatekok
    {
        public int Jatek_id { get; private set; }
        public string Jatek_nev { get; private set; }
        public int Kiado_id { get; private set; }
        public string Mufaj { get; private set; }
        public DateTime Megjelenesi_Datum { get; private set; }
        public string Platform { get; private set; }
        public double Ar { get; private set; }
        public int Eladott_db { get; private set; }
        public int Ertekeles { get; private set; }

    }
}
