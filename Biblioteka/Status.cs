using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Status
    {
        public bool Dostepny { get; set; }
        public bool Wypozyczony { get; set; }
        public bool Zarezerwowany { get; set; }
        public string UserName { get; set; } = "None";

        public Status()
        {
            Dostepny = true;
            Wypozyczony = false;
            Zarezerwowany = false;
            UserName = "None";
        }

        public Status(Dostepnosc dostepnosc,string userName)
        {
            ZmienDostepnosc(dostepnosc, userName);
        }

        public void ZmienDostepnosc(Dostepnosc dostepnosc, string userName="None")
        {
            if (dostepnosc == Dostepnosc.Dostepna)
            {
                Dostepny = true;
                Wypozyczony = false;
                Zarezerwowany = false;
                UserName = "None";
            }
            else if (dostepnosc == Dostepnosc.Zarezerwowana)
            {
                Dostepny = false;
                Wypozyczony = false;
                Zarezerwowany = true;
                UserName = userName;
            }
            else if (dostepnosc == Dostepnosc.Wypozyczona)
            {
                Dostepny = false;
                Wypozyczony = true;
                Zarezerwowany = false;
                UserName = userName;
            }
        }
        public string SprawdzDostepnosc()
        {
            if (Dostepny)
            {
                return "Książka jest dostępna.";
            }
            else if (Wypozyczony)
            {
                return $"Książka jest wypożyczona przez {UserName}";
            }
            else if (Zarezerwowany)
            {
                return $"Książka jest zarezerwowana przez {UserName}";
            }
            else
                return "";
        }
    }
}
