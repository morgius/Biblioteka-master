using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biblioteka
{
    class Biblioteka
    {
        
        private List<Ksiazka> ListaKsiazek { get; set; } 
        private Obslugatxt Obslugatxt { get; set; } 
        private Obslugajson Obslugajson { get; set; } 
        private Obslugaxml Obslugaxml { get; set; } 
        public Biblioteka(Obslugatxt obsugatxt, Obslugajson obslugajson,Obslugaxml obslugaxml)
        {
            Obslugatxt = obsugatxt;
            Obslugajson = obslugajson;
            Obslugaxml = obslugaxml;
            ListaKsiazek = new List<Ksiazka>();
        }
        
        public void Read()
        {
            ListaKsiazek = Obslugaxml.Read(Obslugaxml.path);
        }
        public void Write()
        {
            Obslugajson.Write(ListaKsiazek);
            Obslugatxt.Write(ListaKsiazek);
            Obslugaxml.Write(ListaKsiazek);
        }
        private void Add(Ksiazka ksiazka)
        {
            Obslugatxt.Add(ksiazka);
            Obslugajson.Add(ksiazka);
            Obslugaxml.Add(ksiazka);
        }
        private void Remove(int id)
        {
            Obslugatxt.Remove(id);
            Obslugajson.Remove(id);
            Obslugaxml.Remove(id);
        }
        private void Update(Ksiazka ksiazka)
        {
            Obslugaxml.Update(ksiazka);
            Obslugatxt.Update(ksiazka);
            Obslugajson.Update(ksiazka);
        }
        
        public void WybierzAkcje()
        {

            ConsoleKeyInfo keyInput;
            do
                {
                Console.WriteLine("Wciśnij:\n1- Wyświetl listę książek.\n2- Dodaj książkę.\n3- Usuń książkę\n4- Zaktualizuj książkę\n" +
                    "5- Zarezerwuj książkę.\n6- Wypożycz książkę.\n7- Zwróć książkę.\nESC- Zakończ pracę.");
                    keyInput = Console.ReadKey();
                    Console.Clear();
                    switch (keyInput.Key)
                    {
                        case ConsoleKey.D1:
                        WydrukujListeKsiazek();
                            break;
                        case ConsoleKey.D2:
                            Console.WriteLine("Dodawanie...");
                            Dodaj();
                            break;
                        case ConsoleKey.D3:
                            Console.WriteLine("Usuwanie...");
                            Usun();
                            break;
                        case ConsoleKey.D4:
                            Console.WriteLine("Aktualizacja...");
                            Aktualizuj();
                            break;
                        case ConsoleKey.D5:
                            Console.WriteLine("Rezerwacja...");
                            Zarezerwuj();
                            break;
                        case ConsoleKey.D6:
                            Console.WriteLine("Wypożyczenie...");
                            Wypozycz();
                            break;
                        case ConsoleKey.D7:
                            Console.WriteLine("Zwrot...");
                            Zwroc();
                            break;
                }
                } while (!(keyInput.Key==ConsoleKey.Escape));
        }
       

        public void WydrukujListeKsiazek()
        {
            foreach (var ksiazka in ListaKsiazek)
            {
                Console.WriteLine(ksiazka.ToString());
            }
        }
        public void Wypozycz()
        {
            string tytul = WprowadzTytul();
            Console.WriteLine("Podaj imie i nazwisko");
            string userName = Console.ReadLine();
            var ksiazkaNaLiscie = ListaKsiazek.Where(k => k.Nazwa.ToLower() == tytul.ToLower()).FirstOrDefault();
            if (ksiazkaNaLiscie is null)
            {
                Console.WriteLine("Nie ma takiej książki");
            }
            else if(ksiazkaNaLiscie.Status.Dostepny ==true)
            {
                Console.WriteLine("Ksiazka jest dostepna. Wypożyczono.");
                Ksiazka wypozyczonaKsiazka = new Ksiazka(ksiazkaNaLiscie.Id, ksiazkaNaLiscie.Nazwa, ksiazkaNaLiscie.Kategoria,
                                                new Status {Dostepny=false,Wypozyczony=true,Zarezerwowany=false,UserName=userName});
                Update(wypozyczonaKsiazka);
                Read();
            }
            else if (ksiazkaNaLiscie.Status.Wypozyczony==true)
            {
                Console.WriteLine($"Ksiazka jest wypożyczona przez {ksiazkaNaLiscie.Status.UserName}");
            }
            else if ((ksiazkaNaLiscie.Status.Zarezerwowany==true)&&(ksiazkaNaLiscie.Status.UserName.ToLower()==userName.ToLower()))
            {
                Console.WriteLine("Ksiazka jest prze Ciebie zarezerwowana. Wypożyczono.");
                Ksiazka wypozyczonaKsiazka = new Ksiazka(ksiazkaNaLiscie.Id, ksiazkaNaLiscie.Nazwa, ksiazkaNaLiscie.Kategoria,
                                                new Status { Dostepny = false, Wypozyczony = true, Zarezerwowany = false, UserName = userName });
                Update(wypozyczonaKsiazka);
                Read();
            }
            
        }
        public void Zarezerwuj()
        {
            string tytul = WprowadzTytul();
            var ksiazkaNaLiscie = ListaKsiazek.Where(k => k.Nazwa.ToLower() == tytul.ToLower()).FirstOrDefault();
            if(ksiazkaNaLiscie is null)
            {
                Console.WriteLine("Nie ma takiej książki");
            }
            else if (ksiazkaNaLiscie.Status.Dostepny == true)
            {
                Console.WriteLine("Ksiazka jest dostepna. Podaj swoje imię i nazwisko");
                string userName = Console.ReadLine();
                Ksiazka zarezerwowanaKsiazka = new Ksiazka(ksiazkaNaLiscie.Id, ksiazkaNaLiscie.Nazwa, ksiazkaNaLiscie.Kategoria, 
                                              new Status { Dostepny = false, Wypozyczony = false, Zarezerwowany = true, UserName = userName });
                Update(zarezerwowanaKsiazka);
                Read();
            }
            else if (ksiazkaNaLiscie.Status.Wypozyczony == true)
            {
                Console.WriteLine($"Ksiazka jest wypożyczona przez {ksiazkaNaLiscie.Status.UserName}");
            }
            else if (ksiazkaNaLiscie.Status.Zarezerwowany==true)
            {
                Console.WriteLine($"Książka jest zarezerwowana przez {ksiazkaNaLiscie.Status.UserName}");
            }
        }
        public void Zwroc()
        {
            string tytul = WprowadzTytul();
            Console.WriteLine("Podaj swoje imię i nazwisko");
            string userName = Console.ReadLine();
            var ksiazkaNaLiscie = ListaKsiazek.Where(k => k.Nazwa.ToLower() == tytul.ToLower()).FirstOrDefault();
            if (ksiazkaNaLiscie is null)
            {
                Console.WriteLine("Nie ma takiej książki");
            }
            else if ((ksiazkaNaLiscie.Status.Wypozyczony == true) && (ksiazkaNaLiscie.Status.UserName.ToLower() == userName.ToLower()))
            {
                
                Ksiazka zwroconaKsiazka = new Ksiazka(ksiazkaNaLiscie.Id, ksiazkaNaLiscie.Nazwa, ksiazkaNaLiscie.Kategoria,
                                              new Status { Dostepny = true, Wypozyczony = false, Zarezerwowany = false, UserName = "None" });
                Console.WriteLine("Zwrocono.");
                Update(zwroconaKsiazka);
                Read();
            }
            else if ((ksiazkaNaLiscie.Status.Wypozyczony == true) && (!(ksiazkaNaLiscie.Status.UserName.ToLower() == userName.ToLower())))
            {
                Console.WriteLine($"Ksiazka jest wypożyczona przez {ksiazkaNaLiscie.Status.UserName}");
            }
            else if (ksiazkaNaLiscie.Status.Zarezerwowany == true)
            {
                Console.WriteLine($"Książka jest zarezerwowana przez {ksiazkaNaLiscie.Status.UserName}");
            }
            else
            {
                Console.WriteLine("Ta książka nie jest wypożyczona");
            }
        }
        public void Dodaj()
        {
            Add(KsiazkaAdd());
            Read();
        }
        public void Usun()
        {
            Remove(WprowadzIstniejaceId());
            Read();
        }
        public void Aktualizuj()
        {
            Update(KsiazkaUpdate());
            Read();
        }
        private bool SprawdzId(int id)
        {
            bool czyPrawidloweId;
            var ksiazkaId = ListaKsiazek.Where(k => k.Id == id).FirstOrDefault();
            if (ksiazkaId is null)
            {
                czyPrawidloweId = false;
            }
            else
            {
                czyPrawidloweId = true;
            }
            return czyPrawidloweId;
        }
        private Ksiazka KsiazkaUpdate()
        {
            int id = WprowadzIstniejaceId();
            string tytul = WprowadzTytul();
            Console.WriteLine("Podaj kategorie:");
            string kategoria = WybierzKategorie();
            return new Ksiazka(id, tytul, kategoria, new Status());
        }

        private string WybierzKategorie()
        {
            Regex regex = new Regex(@"^[A-Z]\w+[0-9]{4}$");
            string kategoria = "";
            bool czyKategoriaOk;
            do
            {
                Console.WriteLine("Podaj kategorię w postaci 'Xxxx...xx1234'");
                string tekst = Console.ReadLine();
                Match match = regex.Match(tekst);
                if (match.Success)
                {
                    kategoria = tekst;
                }
                czyKategoriaOk = match.Success;
            } while (!czyKategoriaOk);
            return kategoria;
        }

        private Ksiazka KsiazkaAdd()
        {
            int id = WprowadzUnikalneId();
            string tytul = WprowadzTytul();
            Console.WriteLine("Podaj kategorie:");
            string kategoria = WybierzKategorie();
            return new Ksiazka(id, tytul, kategoria, new Status());
        }

        private static string WprowadzTytul()
        {

            Console.WriteLine("Podaj tytuł:");
            string tytul = Console.ReadLine();
            return tytul;
        }

        private int WprowadzUnikalneId()
        {
            bool czyOkId;
            int nrId;
            do
            {
                bool czyPrawidlowyFormat;
                do
                {
                    Console.WriteLine("Wprowadź prawidlowy numer identyfikacyjny:");
                    czyPrawidlowyFormat = int.TryParse(Console.ReadLine(), out nrId);
                } while (!czyPrawidlowyFormat);
                czyOkId = SprawdzId(nrId);
            } while (czyOkId);
            return nrId;
        }
        private int WprowadzIstniejaceId()
        {
            bool czyOkId;
            int nrId;
            do
            {
                bool czyPrawidlowyFormat;
                do
                {
                    Console.WriteLine("Wprowadź prawidlowy numer identyfikacyjny:");
                    czyPrawidlowyFormat = int.TryParse(Console.ReadLine(), out nrId);
                } while (!czyPrawidlowyFormat);
                czyOkId = SprawdzId(nrId);
            } while (!czyOkId);
            return nrId;
        }
    }
}
