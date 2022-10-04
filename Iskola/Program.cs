using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iskola
{
    internal class Program
    {
        class Iskola
        {
            public int Ev { get; set; }
            public char Osztaly { get; set; }
            public string Nev { get; set; }

            public Iskola(int ev, char osztaly, string nev)
            {
                Ev = ev;
                Osztaly = osztaly;
                Nev = nev;
            }
            public Iskola(string beolvas)
            {
                string[] adatok = beolvas.Split(';');
                Ev = int.Parse(adatok[0]);
                Osztaly = char.Parse(adatok[1]);
                Nev = adatok[2];
            }

        }

        class Megoldas
        {
            private List<Iskola> adat;
            private List<string> azonositok;

            public Megoldas()
            {
                adat = new List<Iskola>();
            }

            public void Beolvas(string file)
            {
                string[] reader = File.ReadAllLines(file);
                for (int i = 0; i < reader.Length; i++)
                {
                    adat.Add(new Iskola(reader[i]));
                }
            }

            private List<string> Azonositok()
            {
                List<string> azon = new List<string>();

                string ev;
                string oszt;
                string vez;
                string ker;

                for (int i = 0; i < adat.Count; i++)
                {
                    ev = adat[i].Ev.ToString();
                    ev = ev.Substring(ev.Length - 1);
                    oszt = adat[i].Osztaly.ToString();
                    vez = adat[i].Nev.Substring(0, 3).ToLower();
                    ker = adat[i].Nev.Split(' ')[1].Substring(0, 3).ToLower();
                    azon.Add(ev + oszt + vez + ker);
                }

                return azon;
            }

            static void Feladat(int f)
            {
                Console.Write("{0}. feladat: ", f);
            }

            public void F3()
            {
                Feladat(3);
                Console.WriteLine("Az iskolában {0} tanuló jár.", adat.Count);
            }

            public void F4()
            {
                Feladat(4);
                int hossz;
                string legh_nev = adat[0].Nev.Replace(" ", "");
                for (int i = 1; i < adat.Count; i++)
                {
                    if (adat[i].Nev.Replace(" ", "").Length > legh_nev.Replace(" ", "").Length)
                    {
                        legh_nev = adat[i].Nev;
                    }
                }
                hossz = legh_nev.Replace(" ", "").Length;
                Console.WriteLine("A leghosszabb ({0} karakter) nevű tanuló(k):\n\t{1}", hossz, legh_nev);
            }

            public void F5()
            {
                Feladat(5);
                Console.WriteLine("Azonosítók");
                azonositok = Azonositok();
                Console.WriteLine("\tElső: {0} - {1}", adat[0].Nev, azonositok[0]);
                Console.WriteLine("\tUtolsó: {0} - {1}", adat[adat.Count - 1].Nev, azonositok[1]);
            }

            public void F6()
            {
                Feladat(6);
                Console.Write("Kérek egy azonosítót [pl: 4dvavkri]: ");
                string beker_azon = Console.ReadLine();

                bool talal = false;
                for (int i = 0; i < azonositok.Count; i++)
                {
                    if (beker_azon == azonositok[i])
                    {

                        Console.WriteLine("\t{0} {1} {2}", adat[i].Ev, adat[i].Osztaly, adat[i].Nev);
                        talal = true;
                        break;
                    }
                }
                if (!talal)
                {
                    Console.WriteLine("\tNincs megfelelő tanuló.");
                }

            }

            public void F7()
            {
                Feladat(7);
                Console.WriteLine("Jelszó generálása");
                Random r = new Random();
                JelszóGeneráló jelszo = new JelszóGeneráló(r);
                string jel = jelszo.Jelszó(8);
                int index = r.Next(adat.Count);
                Console.WriteLine("\t{0} - {1}", adat[index].Nev, jel);
            }



        }

        static void Main(string[] args)
        {
            Megoldas fel = new Megoldas();

            fel.Beolvas("nevek.txt");
            fel.F3();
            fel.F4();
            fel.F5();
            fel.F6();
            fel.F7();


            Console.ReadKey();
        }
    }
}
