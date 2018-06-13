﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaneZPlikuOkienko
{
    class Regula
    {
        public int StringToInt(string liczba)
        {
            int wynik;
            if (!int.TryParse(liczba.Trim(), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do int");

            return wynik;
        }
        public int []F_max_czestosc_wartosc_numer_kolumny(List<Najczestszy_atrybut> Lista)
        {
            int[] max = new int[4];
            if (Lista.Count == 0)
            {

            }

            else
            {
                max[0] = Lista[0].czestosc;
                max[1] = StringToInt(Lista[0].wartosc);
                max[2] = Lista[0].nr_atrybutu;
                max[3] = 0;
                for (int i = 0; i < Lista.Count; i++)
                {
                    if (max[0] < Lista[i].czestosc)
                    {
                        max[0] = Lista[i].czestosc;

                        max[1] = StringToInt(Lista[i].wartosc);

                        max[2] = Lista[i].nr_atrybutu;
                        max[3] = i;
                    }
                }
               
            }
            return max;
        }

    public List<string> F_lista_konceptow(string [][] SystemDecyzjyny)
        {
            var lista = new List<string>();
            var kolumna = new string[SystemDecyzjyny.Length];
            int ostatniakolumna = (SystemDecyzjyny[0].Length-1);
            kolumna = Fkolumna(SystemDecyzjyny, ostatniakolumna);
            lista.Add(kolumna[0]);
            for(int i=0;i<kolumna.Length;i++)
            {
                if (!lista.Contains(kolumna[i]))
                    lista.Add(kolumna[i]);
            }
            return lista;
        }

        public List<int> F_wyciagnij_numery_obiektow_konceptu(string[][] Systemdecyzyjny,string Koncept)
        {
            var Lista = new List<int>();
            for(int i=0;i<Systemdecyzyjny.Length;i++)
            {
                if (Systemdecyzyjny[i].Last() == Koncept)
                    Lista.Add(i);
            }
            return Lista;
        }
      

        public string[] Fkolumna(string[][] dane , int numerkolumny)
        {
            var kolumna = new string[dane.Length];
            for(int i=0;i<kolumna.Length;i++)
            {
                kolumna[i] = dane[i][numerkolumny];
            }
            return kolumna;
        }
        public string[] Fwiersz(string[][] dane, int numerwiersza)//z wczytanych danych zwraca wybraną kolumne 
        {
            string[] wiersz = new string[dane[0].Length];
            for (int i = 0; i < wiersz.Length; i++)
            {
                wiersz[i] = dane[numerwiersza][i];
            }
            return wiersz;
        }
        public int[][] Fdwuwymiarowywiersz(int[][][] dane, int numerwiersza)
        {
            int[][] wiersz = new int[dane[0].Length][];

            for (int i = 0; i < wiersz.Length; i++)
            {
                wiersz[i] = dane[numerwiersza][i];
                wiersz[i] = new int[dane[numerwiersza][i].Length];
                for (int j = 0; j < dane[numerwiersza][i].Length; j++)
                {
                    wiersz[i][j] = dane[numerwiersza][i][j];
                }
            }
            return wiersz;
        }

        public Regula Stworz_regule(int argument,string wartosc,string decyzja)
        {
            Regula r = new Regula();
            r.decyzja = decyzja;
            r.deskryptory.Add(argument, wartosc);
            return r;
        }
        public Regula utwurzregule(string[] obiekt, int[] kombinacja)//odniesienie do klasy
        {
            Regula r = new Regula();
            r.decyzja = obiekt.Last();
            for (int i = 0; i < kombinacja.Length; i++)
            {
                int nrAtrybutu = kombinacja[i];
                r.deskryptory.Add(nrAtrybutu, obiekt[nrAtrybutu]);
            }
            return r;
        }
        public int Fsupport(string[][] dane)
        {
            int support = 0;
            for (int i = 0; i < dane.Length; i++)
            {
                if (CzyObiektSpelniaRegule(dane[i]) && decyzja == dane[i].Last())
                {
                    support++;
                }
            }
            return support;
        }
        public List<int> F_ktore_obiekty_spelniaja_regule(string[][] obiekty)
        {
            var lista = new List<int>();
            int pom = 0;
            foreach ( var ob in obiekty)
            {


                if ((this.CzyObiektSpelniaRegule(ob) == true) && (ob.Last() == this.decyzja))
                {
                   
                    lista.Add(pom);
                }
                pom++;
            }
            return lista;
        }

        public bool CzyObiektSpelniaRegule(string[] ob)
        {
            foreach (var desk in this.deskryptory)
            {
                if (ob[desk.Key] != desk.Value)
                    return false;
            }
            return true;
        }

        public bool F_czy_regula_nie_sprzeczna(string[][] obiekty)
        {
            foreach (var ob in obiekty)
            {
                if (this.CzyObiektSpelniaRegule(ob) && ob.Last() != this.decyzja)
                    return false;
            }
            return true;
        }


        

        public int[] TworzKomorke(string[] obiekt1, string[] obiekt2)
        {
            List<int> komorka = new List<int>();
            if (obiekt1.Last() == obiekt2.Last())
                return komorka.ToArray();

            for (int i = 0; i < obiekt1.Length - 1; i++)
            {
                if (obiekt1[i] == obiekt2[i])
                    komorka.Add(i);
            }
            return komorka.ToArray();
        }
        public int[][][] MacierzNieodroznialnosci(string[][] obiekty)
        {
            int[][][] macierz = new int[obiekty.Length][][];
            for (int i = 0; i < obiekty.Length; i++)
            {
                macierz[i] = new int[obiekty.Length][];
                for (int j = 0; j < obiekty.Length; j++)
                {
                    macierz[i][j] = TworzKomorke(obiekty[i], obiekty[j]);
                }
            }
            return macierz;
        }

        public bool CzyKombinacjaWKomorce(int[] komorka, int[] kombinacja)
        {
            for (int i = 0; i < kombinacja.Length; i++)
            {
                if (!komorka.Contains(kombinacja[i]))
                    return false;
            }
            return true;
        }

        public bool CzyZawieraWWierszu(int[][] wiersz, int[] kombinacja)
        {
            for (int i = 0; i < wiersz.Length; i++)
            {
                if (CzyKombinacjaWKomorce(wiersz[i], kombinacja))
                    return true;
            }
            return false;
        }

        public bool CzyRegulaZawieraInnaRegule(Regula r1, Regula r2)
        {
            foreach (var desk in r2.deskryptory)
            {
                if (!r1.deskryptory.ContainsKey(desk.Key) || r1.deskryptory[desk.Key] != desk.Value)
                    return false;
            }
            return true;
        }

    

        public bool CzyRegulaZawieraReguleZListy(List<Regula> lista, Regula r)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (!lista.Contains(r))
                {
                    return false;
                }
            }
            return true;
        }
        public Dictionary<int, string> deskryptory = new Dictionary<int, string>();
        public string decyzja;
        public int support;

        public override string ToString()
        {
            string wynik = string.Format("(a{0}={1})", this.deskryptory.First().Key + 1, this.deskryptory.First().Value);

            for (int i = 1; i < this.deskryptory.Count; i++)
            {
                var kvp = this.deskryptory.ElementAt(i);
                wynik += string.Format(" ^ (a{0}={1})", kvp.Key + 1, kvp.Value);
            }

            wynik += string.Format("=>(D={0})", this.decyzja);
            if (this.support > 1)
                wynik += $"[{this.support}]";

            return wynik;
        }
    }
}
