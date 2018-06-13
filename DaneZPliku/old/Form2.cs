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
        public Form2(string[][] systemDecyzyjny)
        {

            InitializeComponent();
            Regula r = new Regula();
            Najczestszy n = new Najczestszy();
            var Lista_Regul = new List<Regula>();
            var Lista_najczejsciej_wystepujacyh_argumentow = new List<Najczestszy>();
            var Lista_unikalnych_decyzji = new List<string>();
            var Lista_obiektow_konceptu = new List<int>();//jeden koncept(wszystko na jednym potem następne)
            var Lista_obiektow_niepokrytych = new List<int>();
            var Lista_obiektow_spelniajacych_regula_w_danej_chwili = new List<int>();

            Lista_unikalnych_decyzji = r.Flistakonceptow(systemDecyzyjny);//lista unikalnych konceptow
            for (int i = 0; i < Lista_unikalnych_decyzji.Count; i++)//ilosc unikalnych deczyjii
            {
                Lista_obiektow_konceptu = r.Fwyciagnij_numery_obiektow_konceptu(systemDecyzyjny, Lista_unikalnych_decyzji[i]);//mamy liste zawierajaca numery obiektow danego konceptu
                poczatek:
                if(Lista_obiektow_konceptu.Count==0)
                {
                    goto zmienkoncept;
                }
                for (int j = 0; j < Lista_obiektow_konceptu.Count; j++)//OBIEKTY
                {
                    string[] kolumna_konceptu = n.Kolumna_konceptu(systemDecyzyjny, Lista_obiektow_konceptu, j);
                    n = n.Futwurz_najczesciej_wystepujaca_wartos_w_danej_kolumnie(kolumna_konceptu, j);
                    Lista_najczejsciej_wystepujacyh_argumentow.Add(n);
                    //lista najczeszcziej wystepujacyhc argumentow i ich wartosci 
                    //Lista unikalnych decyzjii
                    //Lista zawierajaca numery obiektow na ktore patrzymy
                    
                }
                var Tab_Czestosc_Wartosc_NumerKol = new int[3];
                Tab_Czestosc_Wartosc_NumerKol = r.Max_Czestosc_Wartosc_NumerKol(Lista_najczejsciej_wystepujacyh_argumentow);//wiem ile wiem jakie i ktory argument
                r = r.stworzregule(Tab_Czestosc_Wartosc_NumerKol[2],Convert.ToString(Tab_Czestosc_Wartosc_NumerKol[1]),Lista_unikalnych_decyzji[i]);
                Lista_Regul.Add(r);
               // var Lista_Obiektow_spelniajacych_regule = new List<int>();
                Lista_obiektow_spelniajacych_regula_w_danej_chwili = r.KtoreObiektySpelniajaRegule(systemDecyzyjny, r);//tylko te z danego konceptu
                //goto Start;
                //for(int k=0;k<Lista_obiektow_spelniajacych_regula_w_danej_chwili.Count;k++)
                Start:
                if(r.CzyRegulaNieSprzeczna(r,systemDecyzyjny)==true)
                {
                    Lista_Regul.Add(r);
                    for(int x=0;x<Lista_obiektow_spelniajacych_regula_w_danej_chwili.Count;x++)
                    Lista_obiektow_konceptu.Remove(Lista_obiektow_spelniajacych_regula_w_danej_chwili[x]);
                    goto poczatek;
                }
                else
                {
                    Lista_najczejsciej_wystepujacyh_argumentow.Clear();//czyszczę listę przed zmiana konceptu
                    for (int j = 0; j < Lista_obiektow_spelniajacych_regula_w_danej_chwili.Count -1; j++)
                    {
                        string[] kolumna_konceptu = n.Kolumna_konceptu(systemDecyzyjny, Lista_obiektow_spelniajacych_regula_w_danej_chwili, j);
                        n = n.Futwurz_najczesciej_wystepujaca_wartos_w_danej_kolumnie(kolumna_konceptu, j);
                        Lista_najczejsciej_wystepujacyh_argumentow.Add(n);
                    }
                    Tab_Czestosc_Wartosc_NumerKol = r.Max_Czestosc_Wartosc_NumerKol(Lista_najczejsciej_wystepujacyh_argumentow);//wiem ile wiem jakie i ktory argument
                    r.deskryptory.Add(Tab_Czestosc_Wartosc_NumerKol[2],Convert.ToString(Tab_Czestosc_Wartosc_NumerKol[1]));
                    // r = r.stworzregule(Tab_Czestosc_Wartosc_NumerKol[2], Convert.ToString(Tab_Czestosc_Wartosc_NumerKol[1]), Lista_unikalnych_decyzji[i]);
                    // var Lista_Obiektow_spelniajacych_regule = new List<int>();
                    Lista_obiektow_spelniajacych_regula_w_danej_chwili = r.KtoreObiektySpelniajaRegule(systemDecyzyjny, r);//tylko te z danego konceptu
                    goto Start;
                }                   
                    zmienkoncept:
                {
                    Lista_najczejsciej_wystepujacyh_argumentow.Clear();//czyszczę listę przed zmiana konceptu
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
