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
        public byte Ertekeles { get; private set; }
        public Kiadok Kiado { get; private set; }

        public Jatekok(string sor, Kiadok k)
        {
            string[] tpm = sor.Split(',');
            Jatek_id = Convert.ToInt32(tpm[0]);
            Jatek_nev = tpm[1];
            Kiado_id = Convert.ToInt32(tpm[2]);
            Mufaj = tpm[3];
            Megjelenesi_Datum = Convert.ToDateTime(tpm[4]);
            Platform = tpm[5];
            Ar = Convert.ToDouble(tpm[6].Replace('.', ','));
            Eladott_db = Convert.ToInt32(tpm[7]);
            Ertekeles = Convert.ToByte(tpm[8]);
            Kiado = k;



        }

        public double Bevetel()
        {
            return Eladott_db * Ar;
        }

        public override string ToString()
        {
            return $"Játék_neve:{Jatek_nev} Kiadó: {Kiado.Kiado_nev} Múfaj: {Mufaj} Megj.Dát.:{Megjelenesi_Datum.ToString("yyyy.MM_:dd")} Platform: {Platform} Ár:{Ar.ToString("f2")}$ Értékelés:100/{Ertekeles}";
           
        }
    }
}
