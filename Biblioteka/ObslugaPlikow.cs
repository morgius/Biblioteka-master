using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public abstract class ObslugaPlikow
    {
        
      
        public abstract List<Ksiazka> Read(string path);
        public abstract void Write(List<Ksiazka> lista);
        public abstract void Update(Ksiazka ksiazka);
        public abstract void Remove(int id);
        public abstract void Add(Ksiazka ksiazka);
        
        public void Updatowanie(Ksiazka ksiazka, List<Ksiazka> listaKsiazek)
        {
            var updatowanaKsiazka = listaKsiazek.FirstOrDefault(x => x.Id == ksiazka.Id);
            listaKsiazek.Remove(updatowanaKsiazka);
            listaKsiazek.Add(ksiazka);
            listaKsiazek.Sort();
            Write(listaKsiazek);
        }
        public void Usuwanie(int id, List<Ksiazka> listaKsiazek)
        {
            var usuwanaKsiazka = listaKsiazek.FirstOrDefault(x => x.Id == id);
            if (listaKsiazek.Contains(usuwanaKsiazka))
            {
                listaKsiazek.Remove(usuwanaKsiazka);
                listaKsiazek.Sort();
            }
            Write(listaKsiazek);
        }
        public void Dodawanie(Ksiazka ksiazka, List<Ksiazka> listaKsiazek)
        {
            listaKsiazek.Add(ksiazka);
            listaKsiazek.Sort();
            Write(listaKsiazek);
        }
    }
}
