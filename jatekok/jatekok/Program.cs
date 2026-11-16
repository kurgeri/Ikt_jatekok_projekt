using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace jatekok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Kiadok> kiadok_lista = new List<Kiadok>();
            List<Jatekok> jatekok_lista = new List<Jatekok>();
            try
            {
            
                foreach (string sor in File.ReadAllLines("publishers.csv").Skip(1))
                {
                    kiadok_lista.Add(new Kiadok(sor));
                }

                foreach (string sor in File.ReadAllLines("videogames.csv").Skip(1))
                {
                    jatekok_lista.Add(new Jatekok(sor));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("FATÁLIS HIBA!\n Valamelyik oknál fogva nem tud megfelelőlen elindulni a program!. A program most bezár.");
                Console.WriteLine($"Hiba kód: {e}");
                Environment.Exit(0);
            }



            Menu(jatekok_lista, kiadok_lista);
        }

        static void Menu(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.Clear();
            Console.WriteLine("Üdvözlöm!\nMit szeretne megtekinteni?");
            Console.Write("K: Keresés játékra\nS: Szűrés\nA: Adatok\nKi: Kilépés\n:");
            string bekert = string.Empty;
            do {
                bekert = Console.ReadLine().ToUpper();

                switch (bekert)
                {
                    case "K":
                        Kereses(j_lista, k_lista);
                        break;
                    case "S":
                        Szures(j_lista, k_lista);
                        break;
                    case "A":
                        Adatok(j_lista, k_lista);
                        break;
                    case "KI":
                        Kilepes();
                        break;
                    default:
                        Console.Write("Nem megfelelő adat!\nAdja meg újra: ");
                        break;
                }
            } while (bekert != "K" && bekert != "S" && bekert != "KI" && bekert != "A");


        }

        static void Kereses(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.Clear();
            Console.Write("Adja meg a játék nevét amire rászeretne keresni:");
            string bekert = string.Empty;
            bool bennevan_e = false;

            do
            {

                bekert = Console.ReadLine().ToLower();

                bennevan_e = j_lista.Any(x => x.Jatek_nev.ToLower().StartsWith(bekert));
                if (bennevan_e == true)
                {
                    List<Jatekok> jatekok = j_lista.Where(x => x.Jatek_nev.ToLower().StartsWith(bekert)).ToList();
                    Console.WriteLine($"Találatok száma: {jatekok.Count()}");
                    Console.WriteLine("------------------");
                    jatekok.ForEach(x => Console.WriteLine(x));


                }
                else
                {
                    Console.WriteLine("Nincs találat!\nMegszeretné újra adni? (I/N)");
                    char opcio;
                    do
                    {
                        try
                        {
                            opcio = Convert.ToChar(Console.ReadLine().ToUpper());
                        }
                        catch
                        {
                            opcio = ' ';
                        }
                        switch (opcio)
                        {
                            case 'I':
                                Kereses(j_lista, k_lista);
                                break;
                            case 'N':
                                Menu(j_lista, k_lista);
                                break;
                            default:
                                Console.WriteLine("Hiba!");
                                break;
                        }

                    } while (opcio != 'N' && opcio != 'I');

                }
            } while (bennevan_e == false);
            Console.WriteLine("\nNyomjon meg egy gombot, hogy visszamenjen a menübe");
            Console.ReadKey();
            Menu(j_lista, k_lista);


        }


        static void Adatok(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.Clear();
            Console.WriteLine("Néhány általános adat:");
            Console.WriteLine($"Játékok darab száma:{j_lista.Count()}\nKiadók darab száma: {k_lista.Count()}");
            Console.WriteLine("------------------");
            Console.Write($"Van-e solo fejlesztő? \t");
            Console.WriteLine(k_lista.Any(x => x.Alkamazottak_db == 1) ? "Igen, van!" : "Nem, nincs");
            Console.WriteLine("------------------");
            var cegek_bevelete_jatekokent = k_lista.Join(j_lista, k => k.Kiado_id, j => j.Kiado_id, (k, j) => new
            {
                k.Kiado_nev,
                j.Jatek_nev,
                Bevetel = j.Bevetel(),
            }).OrderByDescending(x=>x.Bevetel).ToList();
            Console.WriteLine($"Statisztika: Adott cég mennyi játékot adott ki:\n");
            Console.WriteLine("Nyomjon meg egy gombot a folytatáshoz!");
            Console.ReadKey();
            Dictionary<string, int> ceg_db = cegek_bevelete_jatekokent.GroupBy(x => x.Kiado_nev).OrderByDescending(x => x.Count()).ToDictionary(x => x.Key, y => y.Count());
            foreach(KeyValuePair<string,int> kvp in ceg_db)
            {
                Console.WriteLine($"Név:\t{kvp.Key}\tDb:\t  {kvp.Value}");
            }
            Console.WriteLine("------------------");
            Console.WriteLine($"Ha kíváncsi, hogy a cégeknek egy adott játék mennyit hozott, tekintse meg a Cegek_bevetele_jatekokent.txt állományt! ");
            try
            {
                File.WriteAllLines("Cegek_bevetele_jatekokent.txt", cegek_bevelete_jatekokent.Select(x => x.Kiado_nev + $";{x.Jatek_nev};{x.Bevetel}"));
            }
            catch(Exception e)
            {
                Console.WriteLine("HIBA! Nem futott le a fájlba írás");
                Console.WriteLine($"hiba kód: {e}");
            }
          
            Console.WriteLine("------------------");
            Console.Write("Nyomjon meg egy gombot, hogy visszamenjen a menübe");
            Console.ReadKey();
            Console.Clear();
            Menu(j_lista, k_lista);
        }
        //Szűrések
        static void Szures(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.Clear();
            Console.WriteLine("Mire szeretne szűrni?");
            Console.WriteLine("J: Játék\nC: Cég");
            string bekert = string.Empty;
            do {
                bekert = Console.ReadLine().ToUpper();
                switch (bekert)
                {
                    case "J":
                        Szures_Jatek(j_lista, k_lista);
                        break;
                    case "C":
                        Szures_Ceg(j_lista, k_lista);
                        break;
                    default:
                        Console.Write("Hiba!\n Adja meg újra:");
                        break;
                }
            } while (bekert != "J" && bekert != "C");
        }
        static void Szures_Ceg(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.Clear();
            Console.WriteLine("Cég szűrés\n\nMire szeretne szűrni?");
            Console.Write("A: Alapítás\nN: Nettó vagyon\nAl: Alkamazottak\n:");
            string bekert = string.Empty;
            do
            {
                bekert = Console.ReadLine().ToUpper();
                switch (bekert)
                {
                    case "A":
                        Szures_ceg_Alapitas(j_lista, k_lista);
                        break;
                    case "N":
                        Szures_ceg_Netto(j_lista, k_lista);
                        break;
                    case "AL":
                        Szures_ceg_Alkamazottak(j_lista, k_lista);
                        break;
                }

            } while (bekert != "A" && bekert != "N" && bekert != "AL");

            char beker = ' ';
            do
            {
                Console.Write("Szeretne még szűrni? (I/N) \n:");
                try
                {
                    beker = Convert.ToChar(Console.ReadLine().ToUpper());
                }
                catch
                {
                    beker = 'a';
                }
                switch (beker)
                {
                    case 'I':
                        Szures(j_lista, k_lista);
                        break;
                    case 'N':
                        Console.Write("Nyomjon meg egy gombot, hogy visszamenjen a menübe");
                        Console.ReadKey();
                        Menu(j_lista, k_lista);
                        break;
                    default:
                        Console.WriteLine("Hiba!");
                        break;
                }
            } while (beker != 'I' && beker != 'N');

        }
        static void Szures_ceg_Alapitas(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.WriteLine($"Legújabban alakult cég:\n{k_lista.Where(x => x.Allapitas_datuma == k_lista.Max(y => y.Allapitas_datuma)).Select(x => $"{x.Kiado_nev} Alapítás dátuma: {x.Allapitas_datuma.ToString("yyyy.MM.dd")}").First()}");
            Console.WriteLine("------------------");
            Console.WriteLine($"Legrégebbóta alakult cég:\n{k_lista.Where(x => x.Allapitas_datuma == k_lista.Min(y => y.Allapitas_datuma)).Select(x => $"{x.Kiado_nev} Alapítás dátuma:{x.Allapitas_datuma.ToString("yyyy.MM.dd")}").First()}");
            Console.WriteLine("------------------");
        }
        static void Szures_ceg_Netto(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.WriteLine($"Legvagyonosabb cég:\n{k_lista.Where(x => x.Netto_vagyon == k_lista.Max(y => y.Netto_vagyon)).Select(x => $"{x.Kiado_nev} Nettó vagyon: ${x.Netto_vagyon.ToString("f2")} millió").First()}");
            Console.WriteLine("------------------");
            Console.WriteLine($"Legkevesebb vagyonnal rendelkező cég:\n{k_lista.Where(x => x.Netto_vagyon == k_lista.Min(y => y.Netto_vagyon)).Select(x => $"{x.Kiado_nev} Nettó vagyon: ${x.Netto_vagyon.ToString("f2")} millió").First()}");
            Console.WriteLine("------------------");
            Console.WriteLine($"Cégek átlag vagyona: ${k_lista.Average(x=>x.Netto_vagyon).ToString("f2")} millió\nCégek össz vagyona: ${k_lista.Sum(x=>x.Netto_vagyon)} millió");
        }
        static void Szures_ceg_Alkamazottak(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.WriteLine($"Legtöbb alkamazotatt foglalkozató cég(ek):");
            k_lista.Where(x => x.Alkamazottak_db == k_lista.Max(y => y.Alkamazottak_db)).Select(x => $"{x.Kiado_nev} Dolgozók száma: {x.Alkamazottak_db} db").ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("------------------");
            Console.WriteLine($"Legkevesebb alkamazotatt foglalkozató cég(ek):");
             k_lista.Where(x => x.Alkamazottak_db == k_lista.Min(y => y.Alkamazottak_db)).Select(x => $"{x.Kiado_nev} Dolgozók száma: {x.Alkamazottak_db} db").ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("------------------");
            Console.WriteLine($"Átlagos dolgozók szána: {k_lista.Average(x => x.Alkamazottak_db).ToString("f2")} db \nÖsszes alkalmazott száma: {k_lista.Sum(x => x.Alkamazottak_db)} db");
        }
        static void Szures_Jatek(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.Clear();
            Console.WriteLine("Játékra szűrés\n\nMire szeretne szűrni?");
            Console.Write("Á: Ár\nE: Eladott db\nÉ: Értékelés\nM: Megjelenés\n:");
            string bekert = string.Empty;
            do
            {
                bekert = Console.ReadLine().ToUpper();
                switch (bekert)
                {
                    case "Á":
                        Szures_jatek_Ar(j_lista);
                        break;
                    case "E":
                        Szures_jatek_Eladott(j_lista);
                        break;
                    case "É":
                        Szures_jatek_Ertekeles(j_lista);
                        break;
                    case "M":
                        Szures_jatek_Megjelenes(j_lista);
                        break;
                    default:
                        Console.Write("Hiba!\n Adja meg újra:");
                        break;
                }
            } while (bekert != "Á" && bekert != "E" && bekert != "É" && bekert != "M");
            char beker = ' ';
            do
            {
                Console.Write("Szeretne még szűrni? (I/N) \n:");
                try
                {
                    beker = Convert.ToChar(Console.ReadLine().ToUpper());
                }
                catch
                {
                    beker = 'a';
                }
                switch (beker)
                {
                    case 'I':
                        Szures(j_lista, k_lista);
                        break;
                    case 'N':
                        Console.Write("Nyomjon meg egy gombot, hogy visszamenjen a menübe");
                        Console.ReadKey();
                        Menu(j_lista, k_lista);
                        break;
                    default:
                        Console.WriteLine("Hiba!");
                        break;
                }
            } while (beker != 'I' && beker != 'N');
        }
        static void Szures_jatek_Ar(List<Jatekok> j_lista)
        {
            Console.WriteLine("Legdrágább játék(ok):");

            j_lista.Where(x => x.Ar == j_lista.Max(y => y.Ar)).Select(x => $"{x.Jatek_nev} - ${x.Ar}").ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("------------------");
            Console.WriteLine("Legolcsóbb játék(ok):");

            j_lista.Where(x => x.Ar == j_lista.Min(y => y.Ar)).Select(x => $"{x.Jatek_nev} - ${x.Ar}").ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("------------------");
            Console.WriteLine($"Játékok átlag ára: ${j_lista.Average(x => x.Ar).ToString("f2")}\nAz összes játék ára: ${j_lista.Sum(x => x.Ar).ToString("f2")}");
        }
        static void Szures_jatek_Eladott(List<Jatekok> j_lista)
        {
            string legtobbet_eladott_nev = j_lista.Where(x => x.Eladott_db == j_lista.Max(y => y.Eladott_db)).Select(x => x.Jatek_nev).First();
            double legtobb_db = j_lista.Where(x => x.Eladott_db == j_lista.Max(y => y.Eladott_db)).Select(x => x.Eladott_db).First();
            Console.WriteLine($"Legtöbbet eladott játék:\n{legtobbet_eladott_nev} Mennyiség: {legtobb_db} millió db");
            Console.WriteLine("------------------");
            string legkevesebbet_eladott_nev = j_lista.Where(x => x.Eladott_db == j_lista.Min(y => y.Eladott_db)).Select(x => x.Jatek_nev).First();
            double legkevesebb_db = j_lista.Where(x => x.Eladott_db == j_lista.Min(y => y.Eladott_db)).Select(x => x.Eladott_db).First();
            Console.WriteLine($"Legkevesebett eladott játék:\n{legkevesebbet_eladott_nev} Mennyiség: {legkevesebb_db} millió db");
            Console.WriteLine("------------------");
            double atlag = j_lista.Average(x => x.Eladott_db);
            double ossz = j_lista.Sum(x => x.Eladott_db);
            Console.WriteLine($"Játékok átlag eladása: {atlag.ToString("f2")} millió db\nAz összes játék eladása: {ossz.ToString("f2")} millió db ");
        }
        static void Szures_jatek_Ertekeles(List<Jatekok> j_lista)
        {
            Console.WriteLine("Legmagasabbra értékelt játék(ok):");

            j_lista.Where(x => x.Ertekeles == j_lista.Max(y => y.Ertekeles)).Select(x => $"{x.Jatek_nev}; Értékelés:{x.Ertekeles}").ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("------------------");
            Console.WriteLine("Legalacsonyabbra értékelt játék(ok):");
            j_lista.Where(x => x.Ertekeles == j_lista.Min(y => y.Ertekeles)).Select(x => $"{x.Jatek_nev}; Értékelés:{x.Ertekeles}").ToList().ForEach(x => Console.WriteLine(x));

            Console.WriteLine("------------------");
            Console.WriteLine($"Játékok átlag értékelése: 100/{j_lista.Average(x => x.Ertekeles).ToString("f2")}");
        }
        static void Szures_jatek_Megjelenes(List<Jatekok> j_lista)
        {
            Console.WriteLine($"Legfrissebb játék:\n{j_lista.Where(x => x.Megjelenesi_Datum == j_lista.Max(y => y.Megjelenesi_Datum)).Select(x => $"{x.Jatek_nev} Megjelenési idő: {x.Megjelenesi_Datum.ToString("yyyy.MM.dd")}").First()}");
            Console.WriteLine("------------------");
            Console.WriteLine($"Legrégebbi játék:\n{j_lista.Where(x => x.Megjelenesi_Datum == j_lista.Min(y => y.Megjelenesi_Datum)).Select(x => $"{x.Jatek_nev} Megjelenési idő: {x.Megjelenesi_Datum.ToString("yyyy.MM.dd")}").First()}");
            Console.WriteLine("------------------");
        }


        static void Kilepes()
        {
            Console.Clear();
            Console.WriteLine("Viszontlátásra!");
            Environment.Exit(0);
         
        }
    }
}
