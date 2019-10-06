using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public enum Dostepnosc
    {
        Dostepna,
        Wypozyczona,
        Zarezerwowana
    }
    public class Ksiazka:IComparable<Ksiazka>
    {
        public int Id { get; set; }
        public string  Nazwa { get; set; }
        public string Kategoria { get; set; }
        public Status Status { get; set; }

        public Ksiazka(int id,string nazwa,string kategoria, Status status)
        {
            Id = id;
            Nazwa = nazwa;
            Kategoria = kategoria;
            Status = status;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Tytuł: {Nazwa}, Kategoria: {Kategoria}, Status: {Status.SprawdzDostepnosc()}";
        }

        public int CompareTo(Ksiazka other)
        {
            if (this.Id > other.Id)
                return 1;
            else if (this.Id < other.Id)
                return -1;
            else
                return 0;
        }
    }
}
