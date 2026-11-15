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

        public Jatekok(string sor)
        {
            string[] tpm = sor.Split(',');
            Jatek_id = Int_atalakito(tpm[0]);
            Jatek_nev = String_atalakito(tpm[1]);
            Kiado_id = Int_atalakito(tpm[2]);
            Mufaj = String_atalakito(tpm[3]);
            Megjelenesi_Datum = DateTime_atalakito(tpm[4]);
            Platform = String_atalakito(tpm[5]);
            Ar = Double_Atalakito(tpm[6]);
            Eladott_db = Int_atalakito(tpm[7]);
            Ertekeles = Int_atalakito(tpm[8]);



        }
        public double Double_Atalakito(string be)
        {
            double ki;
            char elvalaszto = ' ';
            int seged;
            for (int i = 0; i < be.Length; i++)
            {
            
                try
                {
                    seged = Convert.ToInt32(Convert.ToString(be[i]));
        
                }
                catch
                {
                    elvalaszto = be[i];
                    
                }
            }

            try
            {
                ki = Convert.ToDouble(be.Replace(elvalaszto, ','));
            }
            catch
            {
                ki = -0.1;
            }
            return ki;
        }
        public DateTime DateTime_atalakito(string be)
        {
            DateTime ki;
            try
            {
                ki = Convert.ToDateTime(be);
            }
            catch
            {
                ki = new DateTime(1, 1, 1, 1, 1, 0);
            }
            return ki;
        }
        public string String_atalakito(string be)
        {
            string ki;
            try
            {
                ki = be;
            }
            catch
            {
                ki = string.Empty;
            }
            return ki;
        }

        public int Int_atalakito(string be)
        {
            int ki;
            try
            {
                ki = Convert.ToInt32(be);
            }
            catch
            {
                ki = 0;
            }
            return ki;
        }

        public double Bevetel()
        {
            return Eladott_db * Ar;
        }

        public override string ToString()
        {
            return $"Név:{Jatek_nev} Műfaj: {Mufaj} Megj.Dát.:{Megjelenesi_Datum.ToString("yyyy.MM_:dd")} Platform: {Platform} Ár:{Ar.ToString("f2")}$ Értékelés:100/{Ertekeles}";
           
        }
    }
}
