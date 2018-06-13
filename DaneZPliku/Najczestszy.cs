using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaneZPlikuOkienko
{
    class Najczestszy_atrybut
    {
        
        public int nr_atrybutu;
        public string wartosc;
        public int czestosc;

        public List<int> F_dostepne_atrybuty(string[][] System_decyzyjny)
        {
            var Lista = new List<int>();
            for (int i = 0; i < System_decyzyjny[0].Length - 1; i++)
            {
                Lista.Add(i);
            }
            return Lista;
        }
       
        public Najczestszy_atrybut F_utworz_najczesciej_wystepujaca_wartos_danego_atrybutu(string[] kolumna_wartosc_atrybutu_danego_konceptu,int numer_atrybutu) 
        {
            Najczestszy_atrybut n = new Najczestszy_atrybut();
            n.nr_atrybutu = numer_atrybutu;
            int czestosc = 0;
            var slownik = new Dictionary<string, int>();
            slownik = F_czestosc(kolumna_wartosc_atrybutu_danego_konceptu);
            foreach(var kvp in slownik)
            {
                if(kvp.Value>czestosc)
                {
                    czestosc = kvp.Value;
                    n.czestosc = kvp.Value;
                    n.wartosc = kvp.Key;
                }
            }
            return n;
        }
     
        Dictionary<string,int> F_czestosc (string[] tab)
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
        public string[] F_utworz_kolumne_konceptu_danego_argumentu(string[][] systemDecyzyjny,List<int> numery_ob_konceptu,int numer_kolumny)
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
