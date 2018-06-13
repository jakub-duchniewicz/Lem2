using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kw.Combinatorics;
namespace DaneZPlikuOkienko
{
    //1) Unikalne Decyzje 
    //2)Pobieramy koncept (ustalamy na które obiekty patrzymy) t1
    //3)Lista obiektów niepokrytych t1 obiekty konceptow t2 obiekty niepokrytych t3 obiekty spełniające regułe "Do tej pory" t2
    //4)Obiekty spełniajace regułe t3
    //5)Najcześciej występujaca wartoś argumentów (deskryptorow)
    //6)Dodaje do reguły deskryptor
    //7)Czy reguła niesprzeczna
    //a)Nie => (4)
    //b)Tak => (8)
    //8)Liczenie supportu
    //9)Dodanie reguły do listy reguł końcowych
    //10)czy są niepokryte obiekty 
    //a) Tak => 3
    //b) Nie => 11
    //11)Czy ostatni koncept 
    //a)Nie => skok (2)
    //b)Tak => skok (12)
    //12)Wypisanie reguł
    //13)koniec
    //class Deskryptor { 
    // int nrAtrybutu;
    // string wartosc;
    // int czestosc; maximum z tego }
    public partial class Form2 : Form
    {
        public Form2(string[][] System_decyzyjny)
        {

            InitializeComponent();
            Regula r = new Regula();
            Najczestszy_atrybut n = new Najczestszy_atrybut();
            var Lista_regul = new List<Regula>();
            var Lista_atrybutow_z_wartoscia_i_czestoscia = new List<Najczestszy_atrybut>();
            var Lista_konceptow = new List<string>();
            var Lista_obiektow_danego_konceptu = new List<int>();//jeden koncept(wszystko na jednym potem następne)
            var Lista_obiektow_niepokrytych = new List<int>();
            var Lista_obiektow_spelniajacych_regule_w_danej_chwili = new List<int>();
            var Lista_dostepnych_atrybutow = new List<int>();

            Lista_konceptow = r.F_lista_konceptow(System_decyzyjny);//lista unikalnych konceptow
            for (int i = 0; i < Lista_konceptow.Count; i++)
            {
                Lista_obiektow_danego_konceptu = r.F_wyciagnij_numery_obiektow_konceptu(System_decyzyjny, Lista_konceptow[i]);//mamy liste zawierajaca numery obiektow danego konceptu
                Lista_obiektow_niepokrytych = Lista_obiektow_danego_konceptu;
                poczatek2:
                Lista_dostepnych_atrybutow = n.F_dostepne_atrybuty(System_decyzyjny);
                if (Lista_obiektow_niepokrytych.Count == 0)
                {
                    goto zmienkoncept;
                }
                for (int j = 0; j < System_decyzyjny[0].Length - 1; j++)//po kolumnach
                {
                    string[] kolumna_danego_argumentu_zawierajaca_wartosci_obiektow_konceptu = n.F_utworz_kolumne_konceptu_danego_argumentu(System_decyzyjny, Lista_obiektow_niepokrytych, Lista_dostepnych_atrybutow[j]);
                    n = n.F_utworz_najczesciej_wystepujaca_wartos_danego_atrybutu(kolumna_danego_argumentu_zawierajaca_wartosci_obiektow_konceptu, Lista_dostepnych_atrybutow[j]);
                    Lista_atrybutow_z_wartoscia_i_czestoscia.Add(n);
                    //Lista zawierajaca atrybuty , ich wartość i czestosci maxymalne

                }
                poczatek:
                var Tablica_atrybut_1_wartosc_2_czestosc_3_numer_atrybutu_na_liscie_4_ = new int[4];
                Tablica_atrybut_1_wartosc_2_czestosc_3_numer_atrybutu_na_liscie_4_ = r.F_max_czestosc_wartosc_numer_kolumny(Lista_atrybutow_z_wartoscia_i_czestoscia);//znajduje najczesciej wystepujacy elemnet i pobiera informacje ktory to atrybut , jego wartosc i czestosc
                if (r.deskryptory.Count == 0)//jezeli nie istnieje zaden deskryptor to 
                {
                    r = r.Stworz_regule(Tablica_atrybut_1_wartosc_2_czestosc_3_numer_atrybutu_na_liscie_4_[2], Convert.ToString(Tablica_atrybut_1_wartosc_2_czestosc_3_numer_atrybutu_na_liscie_4_[1]), Lista_konceptow[i]);//tworze regułe
                }
                else //jezeli istnieje deskryptor to 
                {
                    r.deskryptory.Add(Tablica_atrybut_1_wartosc_2_czestosc_3_numer_atrybutu_na_liscie_4_[2], Convert.ToString(Tablica_atrybut_1_wartosc_2_czestosc_3_numer_atrybutu_na_liscie_4_[1]));
                }
               
                if (r.F_czy_regula_nie_sprzeczna(System_decyzyjny))//sprawdzam czy regula nie sprzeczna
                {
                    Lista_obiektow_spelniajacych_regule_w_danej_chwili = r.F_ktore_obiekty_spelniaja_regule(System_decyzyjny);

                    for (int ii = 0; ii < Lista_obiektow_niepokrytych.Count; ii++)
                    {
                        if (Lista_obiektow_spelniajacych_regule_w_danej_chwili.Contains(Lista_obiektow_niepokrytych[ii]))//jezeli lista obiektow spelniajacyh regule nie zawiera obiektu który ma lista obiektow danego konceptu to  
                        {
                            Lista_obiektow_niepokrytych.RemoveAt(ii);//dodajemy obiekt który nie spelnia reguly  do listy niepokrytych obiektow
                                                                                                                       // Lista_obiektow_danego_konceptu.Remove(Lista_obiektow_danego_konceptu[ii]);//usuwamy z lsity obiektow danego konceptu obiekt nie spełniajacy reguły                     
                        }
                    }
                    // for(int k=0;k<)
                    r.support = 0;
                    for (int x = 0; x < Lista_obiektow_spelniajacych_regule_w_danej_chwili.Count; x++)
                    {
                        r.support++;
                        Lista_obiektow_niepokrytych.Remove(Lista_obiektow_spelniajacych_regule_w_danej_chwili[x]);
                    }
                    Lista_regul.Add(r);
                    r = new Regula();
                    Lista_atrybutow_z_wartoscia_i_czestoscia = new List<Najczestszy_atrybut>();
                    //Lista_obiektow_danego_konceptu.Clear();
                   // r.deskryptory.Clear();
                    //Lista_obiektow_danego_konceptu = Lista_obiektow_niepokrytych;
                    
                        goto poczatek2;
                }

               
                else
                {
                    //musimy usunac obiekt niespełniający regułe V
                    //takze usunienty obiekt dodac do listy obiektow niepokrytych  V
                    //wygenerowac nowa liste najczesciej_wystepujaca_wartos_danego_atrybutu ponieważ usuwamyz rozważan obiekt niespełniajacy regułe V
                    // usunac atrybut uzyty do deksryptora V
                   // Lista_obiektow_spelniajacych_regule_w_danej_chwili.Clear();
                    Lista_obiektow_spelniajacych_regule_w_danej_chwili = r.F_ktore_obiekty_spelniaja_regule(System_decyzyjny);
                 
                    for (int jj = 0; jj < Lista_atrybutow_z_wartoscia_i_czestoscia.Count; jj++)
                    {
                        int nr_atrybutu = Lista_atrybutow_z_wartoscia_i_czestoscia[jj].nr_atrybutu;
                        int poprrzedni_atrybut = Tablica_atrybut_1_wartosc_2_czestosc_3_numer_atrybutu_na_liscie_4_[2];
                        if (nr_atrybutu == poprrzedni_atrybut)
                        {
                            //Lista_dostepnych_atrybutow.Add(Lista_atrybutow_z_wartoscia_i_czestoscia[jj]);
                            Lista_dostepnych_atrybutow.RemoveAt(jj);
                            //Lista_atrybutow_z_wartoscia_i_czestoscia.RemoveAt(jj); //usuwa z listy argument nie spełniający reguły
                        }
                        //przekaz w arg
                    }
                    Lista_atrybutow_z_wartoscia_i_czestoscia.Clear();//czyscze liste przed wygenerowanie nowej
                    for (int j = 0; j < Lista_dostepnych_atrybutow.Count -1; j++)//petla po ilosci obiektow danego konceptu
                    {
                        string[] kolumna_danego_argumentu_zawierajaca_wartosci_obiektow_konceptu = n.F_utworz_kolumne_konceptu_danego_argumentu(System_decyzyjny, Lista_obiektow_spelniajacych_regule_w_danej_chwili, Lista_dostepnych_atrybutow[j]);
                        n = n.F_utworz_najczesciej_wystepujaca_wartos_danego_atrybutu(kolumna_danego_argumentu_zawierajaca_wartosci_obiektow_konceptu, Lista_dostepnych_atrybutow[j]);
                        Lista_atrybutow_z_wartoscia_i_czestoscia.Add(n);
                        //Lista zawierajaca atrybuty , ich wartość i czestosci maxymalne

                    }
                    if(Lista_dostepnych_atrybutow.Count==1)
                    {
                        string[] kolumna_danego_argumentu_zawierajaca_wartosci_obiektow_konceptu = n.F_utworz_kolumne_konceptu_danego_argumentu(System_decyzyjny, Lista_obiektow_spelniajacych_regule_w_danej_chwili, Lista_dostepnych_atrybutow[0]);
                        n = n.F_utworz_najczesciej_wystepujaca_wartos_danego_atrybutu(kolumna_danego_argumentu_zawierajaca_wartosci_obiektow_konceptu, Lista_dostepnych_atrybutow[0]);
                        Lista_atrybutow_z_wartoscia_i_czestoscia.Add(n);
                    }
                    goto poczatek;

                }
                zmienkoncept:
                {
                    Lista_atrybutow_z_wartoscia_i_czestoscia.Clear();//czyszczę listę przed zmiana konceptu
                }
            }
            foreach (var kvp in Lista_regul)
                rtbGlowne.Text += string.Format(kvp.ToString()) + Environment.NewLine;
  
    }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void rtbGlowne_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
