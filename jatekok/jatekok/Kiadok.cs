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





        public Kiadok(string sor)
        {
            string[] tpm = sor.Split(',');
            Kiado_id = Convert.ToInt32(tpm[0]);
            Kiado_nev = tpm[1];
            Allapitas_datuma = Convert.ToDateTime(tpm[2]);
            Netto_toke = tpm[3];
            Alkamazottak_db = Convert.ToInt32(tpm[4]);
        }
    }
}
