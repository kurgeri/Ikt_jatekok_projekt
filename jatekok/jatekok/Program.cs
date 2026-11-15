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
            foreach (string sor in File.ReadAllLines("publishers.csv").Skip(1))
            {
                kiadok_lista.Add(new Kiadok(sor));
            }
            List<Jatekok> jatekok_lista = new List<Jatekok>();
            foreach (string sor in File.ReadAllLines("videogames.csv").Skip(1))
            {
                string[] tpm = sor.Split(',');
                foreach (Kiadok k in kiadok_lista)
                {
                    if (k.Kiado_id == Convert.ToInt32(tpm[2]))
                    {
                        jatekok_lista.Add(new Jatekok(sor, k));
                    }
                }

            }



            Menu(jatekok_lista, kiadok_lista);
        }

        static void Menu(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.Clear();
            Console.WriteLine("Üdvözlöm!\nMit szeretne megtekinteni?");
            Console.Write("K: Keresés játékra\nSz: Szűrés\nKi: Kilépés\n:");
            string bekert = string.Empty;
            do {
                bekert = Console.ReadLine().ToUpper();

                switch (bekert)
                {
                    case "K":
                        Kereses(j_lista, k_lista);
                        break;
                    case "SZ":
                        Szures(j_lista, k_lista);
                        break;
                    case "KI":
                        Kilepes();
                        break;
                    default:
                        Console.WriteLine("Nem megfelelő adat!");
                        Console.ReadKey();
                        Menu(j_lista, k_lista);
                        break;
                }
            } while (bekert != "K" || bekert != "SZ" || bekert != "KI");


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

                        catch (Exception e)
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

                    } while (opcio != 'N' || opcio != 'I');

                }
            } while (bennevan_e == false);
            Console.Write("Nyomjon meg egy gombot, hogy visszamenjen a menübe");
            Console.ReadKey();
            Menu(j_lista, k_lista);


        }
        static void Szures(List<Jatekok> j_lista, List<Kiadok> k_lista)
        {
            Console.Clear();


        }
        static void Kilepes()
        {
            Console.Clear();
            Console.WriteLine("Viszontlátásra!");
            Environment.Exit(0);
        }
        
    }
}
