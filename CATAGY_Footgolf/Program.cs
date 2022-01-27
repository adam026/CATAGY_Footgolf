using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATAGY_Footgolf
{
    class Versenyzo
    {
        public string Nev { get; set; }
        public string Kategoria { get; set; }
        public string Egyesulet { get; set; }

        public int[] fordulok = new int[8];
        public int Osszpontszam => OPCounter(fordulok);

        public Versenyzo(string sor)
        {
            var buff = sor.Split(';');
            Nev = buff[0];
            Kategoria = buff[1];
            Egyesulet = buff[2];
            var indexer = 3;
            for (int i = 0; i < 8; i++)
            {
                fordulok[i] = int.Parse(buff[indexer]);
                indexer++;
            }
        }

        private int OPCounter(int[] fordulok)
        {
            Array.Sort(fordulok);
            var counter = 0;
            for (int i = 0; i < fordulok.Length; i++)
            {
                if (i <= 1)
                {
                    if (fordulok[i] > 0)
                    {
                        counter += 10;
                    }
                    else
                        counter += 0;
                }
                else
                {
                    counter += fordulok[i];
                }
            }
            return counter;
        }

    }
    internal class Program
    {
        static List<Versenyzo> versenyzok = new List<Versenyzo>();
        static void Main(string[] args)
        {
            Beolvasas();
            F03();
            F04();
            F06();
            Kiiras();
            F08();
        }

        private static void F08()
        {
            Console.WriteLine("8. Feladat: Egyesület statsiztika: ");
            var statisztika = new Dictionary<string, int>();
            foreach (var versenyzo in versenyzok)
            {
                if (versenyzo.Egyesulet != "n.a.")
                {
                    if (!statisztika.ContainsKey(versenyzo.Egyesulet))
                    {
                        statisztika.Add(versenyzo.Egyesulet, 1);
                    }
                    else
                    {
                        statisztika[versenyzo.Egyesulet] += 1;
                    }
                }
            }

            foreach (var egyesulet in statisztika)
            {
                if (egyesulet.Value > 2)
                {
                    Console.WriteLine($"\t{egyesulet.Key} - {egyesulet.Value} fő");
                }
            }
        }

        private static void Kiiras()
        {
            var sw = new StreamWriter(@"..\..\RES\osszpontFF.txt", false, Encoding.UTF8);
            foreach (var versenyzo in versenyzok)
            {
                if (versenyzo.Kategoria == "Felnott ferfi")
                {
                    sw.WriteLine($"{versenyzo.Nev};{versenyzo.Osszpontszam}");
                }
            }
            sw.Close();
        }

        private static void F06()
        {
            var lpszNoiBajnok = versenyzok.Where(x =>x.Kategoria == "Noi").OrderBy(y => y.Osszpontszam).Last();
            Console.WriteLine($"6. Feladat: A bajnok női versenyző:\n\tNév: {lpszNoiBajnok.Nev}\n\tEgyesület: {lpszNoiBajnok.Egyesulet}\n\tÖsszpont: {lpszNoiBajnok.Osszpontszam}");
        }

        private static void F04()
        {
            var noiVersenyzok = (versenyzok.Where(x => x.Kategoria == "Noi").Count() * 100) / (float)versenyzok.Count;
            Console.WriteLine($"4. Feladat: A női versenyzők aránya: " + String.Format("{0:0.##}",noiVersenyzok) + " %");
        }

        private static void F03()
        {
            Console.WriteLine($"3. Feladat: Versenyzők száma: {versenyzok.Count}");
        }

        private static void Beolvasas()
        {
            using (var sr = new StreamReader(@"..\..\RES\fob2016.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    versenyzok.Add(new Versenyzo(sr.ReadLine()));
                }
            }
        }
    }
}
