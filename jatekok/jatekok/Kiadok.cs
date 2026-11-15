using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jatekok
{
    public class Kiadok
    {
        public int Kiado_id { get; private set; }
        public string Kiado_nev { get; private set; }
        public DateTime Allapitas_datuma { get; private set; }
        public string Netto_toke { get; private set; }

        public int Alkamazottak_db { get; private set; }
    }
}
