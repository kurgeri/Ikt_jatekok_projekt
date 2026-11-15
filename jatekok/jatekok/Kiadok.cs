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
        public double Netto_toke { get; private set; }
        public int Alkamazottak_db { get; private set; }





        public Kiadok(string sor)
        {
            string[] tpm = sor.Split(',');
            Kiado_id = Int_atalakito(tpm[0]);
            Kiado_nev = String_atalakito(tpm[1]);
            Allapitas_datuma = DateTime_atalakito(tpm[2]);
            Netto_toke = Double_atalakito(tpm[3]);
            Alkamazottak_db = Int_atalakito(tpm[4]);
        }
        public double Double_atalakito(string be)
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
    }
}
