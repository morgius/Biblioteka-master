using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Obslugatxt:ObslugaPlikow
    {
        private readonly string path = @"C:\Users\Bezimienny\Source\Repos\Biblioteka-master\Biblioteka\danetxt.txt";
        //string temp = @"C:\Users\Bezimienny\Source\Repos\Biblioteka-master\Biblioteka\danetxt.txt";
        //private readonly string path = ConfigurationManager.AppSettings["filetxt"];
        //public readonly string path = @"C:\Users\Bezimienny\Source\Repos\Biblioteka-master\Biblioteka\danetxt.txt";


        public List<Ksiazka> ListaKsiazek { get; set; }


        public override List<Ksiazka> Read(string path)
        {
            return File.ReadAllLines(path).Select(TxtToKsiazka).ToList();
        }
        public override void Write(List<Ksiazka> lista)
        {
            File.WriteAllText(path,string.Empty);
            
            for (int i = 0; i < lista.Count; i++)
            {
                string temp = KsiazkaToTxt(lista[i]);
                File.AppendAllText(path, temp + Environment.NewLine);
            }
        }
        public override void Update(Ksiazka ksiazka)
        {
            var listaKsiazek = File.ReadAllLines(path).Select(TxtToKsiazka).ToList();
            Updatowanie(ksiazka, listaKsiazek);
            
        }
        public override void Add(Ksiazka ksiazka)
        {
            var listaKsiazek = File.ReadAllLines(path).Select(TxtToKsiazka).ToList();
            Dodawanie(ksiazka, listaKsiazek);
        }

        public override void Remove(int id)
        {
            var listaKsiazek = File.ReadAllLines(path).Select(TxtToKsiazka).ToList();
            Usuwanie(id, listaKsiazek);
        }
        private Ksiazka TxtToKsiazka(string linia)
        {
            var kolumny = linia.Split(',');
            return new Ksiazka(int.Parse(kolumny[0]), kolumny[1], kolumny[2],
                new Status
                {
                    Dostepny = bool.Parse(kolumny[3]),
                    Wypozyczony = bool.Parse(kolumny[4]),
                    Zarezerwowany = bool.Parse(kolumny[5]),
                    UserName = kolumny[6]
                });
        }
        private string KsiazkaToTxt(Ksiazka ksiazka)
        {
            return $"{ksiazka.Id},{ksiazka.Nazwa},{ksiazka.Kategoria},{ksiazka.Status.Dostepny}," +
                $"{ksiazka.Status.Zarezerwowany},{ksiazka.Status.Wypozyczony},{ksiazka.Status.UserName}";
        }
    }
}


