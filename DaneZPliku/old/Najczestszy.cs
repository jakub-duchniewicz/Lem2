using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaneZPlikuOkienko
{
    class Najczestszy
    {
       public int numer_kolumny;
       public int czestosc_wystepowania;
       public string wartosc_najczesciej_wystepujacego_argumentu;

        public Najczestszy Futwurz_najczesciej_wystepujaca_wartos_w_danej_kolumnie(string[] kolumna,int numerArgumentu)//numerArgumentu=NumerObiektu
        {
            Najczestszy n = new Najczestszy();
            n.numer_kolumny = numerArgumentu;
            int czestosc=0;
           // string wartosc;
            var sl = new Dictionary<string, int>();
            sl = fczestosc(kolumna);
            foreach(var kvp in sl)
            {
                if (kvp.Value > czestosc)
                {
                    czestosc = kvp.Value;
                   n.czestosc_wystepowania = kvp.Value;
                   n.wartosc_najczesciej_wystepujacego_argumentu = kvp.Key;
                }
            }
            // = czestosc;
            // = wartosc;
            return n;
        }

        Dictionary<string,int> fczestosc (string[] tab)
        {
            var sl = new Dictionary<string, int>();
            var unikalne = funikalne(tab);
            for (int i = 0; i < unikalne.Length; i++)
            {
                sl.Add(unikalne[i], 0);
            }
                for (int j = 0; j < tab.Length; j++)
                {
                    sl[tab[j]]++; // int liczba=tab[i] sl[liczba]+=1;
                }
            
            return sl;
        }
             string[] funikalne(string[] tab)
        {
            var lista = new List<string>();
            lista.Add(tab[0]);
            for (int i = 1; i < tab.Length; i++)
            {

                if (!lista.Contains(tab[i]))
                    lista.Add(tab[i]);
            }
            return lista.ToArray();
        }
        public string[] Kolumna_konceptu(string[][] systemDecyzyjny,List<int> numery_ob_konceptu,int numer_kolumny)
        {
            int zmienna;
            var Kolumna = new string[numery_ob_konceptu.Count];
            for(int i=0;i<Kolumna.Length;i++)
            {
                zmienna = numery_ob_konceptu[i];
                Kolumna[i] = systemDecyzyjny[zmienna][numer_kolumny];
            }
            return Kolumna;
        }
       
    }
}
