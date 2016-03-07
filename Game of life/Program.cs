using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_life
{
    class Program
    {
        static void Main(string[] args)
        {
            Plansza sut = new Plansza(40);

            Console.ReadKey();

            while (true)
            {
                sut.Wyswietl();
                //Console.ReadKey();    //Skok czasowy po kliknięciu przycisku
                System.Threading.Thread.Sleep(60);  //Wyświetlanie ciągłe
                Console.Clear();
                sut.Nastepny();
                
            }
        }
    }
    
    class Plansza
    {
        private String[,] tab;
        private int rozmiar;
        private String zywa = "O";
        private String martwa = "-";

        public Plansza(int n)
        {
            rozmiar = n;
            tab = new String[n, n];

            for (int y = 0; y < rozmiar; y++)
                for (int x = 0; x < rozmiar; x++)
                    tab[x, y] = martwa;

            Wypelnij();
        }

        public void Wyswietl()
        {
            for(int y = 0; y < rozmiar; y++)
            {
                for(int x = 0; x < rozmiar; x++)
                    Console.Write(tab[x, y] + " ");

                Console.WriteLine();
            }
        }

        private void Wypelnij()
        {
            Random pozycja = new Random();
            int ilosc = pozycja.Next(rozmiar, rozmiar * rozmiar);

            for (int i = 0; i < ilosc; i++)
                tab[pozycja.Next(0, rozmiar), pozycja.Next(0, rozmiar)] = zywa;
        }
        
        public void Nastepny()
        {
            String[,] tab_pom = new String[rozmiar, rozmiar];

            int sasiad = 0;

            for (int y = 0; y < rozmiar; y++)
            {
                for (int x = 0; x < rozmiar; x++)
                {
                    for (int del_y = -1; del_y < 2; del_y++)
                    {
                        for (int del_x = -1; del_x < 2; del_x++)
                        {
                            if (x == 0 && del_x == -1)
                                del_x++;

                            if (y == 0 && del_y == -1)
                                del_y++;

                            if (x == (rozmiar - 1) && del_x == 1)
                                continue;

                            if (y == (rozmiar - 1) && del_y == 1)
                                continue;

                            if (del_x == 0 && del_y == 0)
                                continue;

                            if (tab[x + del_x, y + del_y] == zywa)
                                sasiad++;
                        }
                    }

                    if (sasiad < 2 && tab[x, y] == zywa)
                        tab_pom[x, y] = martwa;
                    else if ((sasiad == 2 || sasiad == 3) && tab[x, y] == zywa)
                        tab_pom[x, y] = zywa;
                    else if (sasiad == 3 && tab[x, y] == martwa)
                        tab_pom[x, y] = zywa;
                    else
                        tab_pom[x, y] = martwa;

                    sasiad = 0;
                }
                    
            }

            //for (int y = 0; y < rozmiar; y++)
            //    for (int x = 0; x < rozmiar; x++)
            //       tab[x, y] = tab_pom[x, y];
            tab = tab_pom;  //Działa bez problemu
        }

    }
}
