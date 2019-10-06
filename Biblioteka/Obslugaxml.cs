using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Biblioteka
{
    public class Obslugaxml:ObslugaPlikow
    {
        //public readonly string path = "danexml.xml";
        public readonly string path = ConfigurationManager.AppSettings["filexml"];

        public List<Ksiazka> ListaKsiazek { get; set; }
        //
        public override List<Ksiazka> Read(string path)
        {
            var document = XDocument.Load(path);
            var listaKsiazek = document.Element("Ksiazki")
                                  .Elements("ksiazka")
                                  .Select(k => new Ksiazka(int.Parse(k.Attribute("Id").Value), k.Attribute("Nazwa").Value,
                                                   k.Attribute("Kategoria").Value, new Status
                                                   {
                                                       Dostepny = bool.Parse(k.Attribute("Dostepny").Value),
                                                       Wypozyczony = bool.Parse(k.Attribute("Wypozyczony").Value),
                                                       Zarezerwowany = bool.Parse(k.Attribute("Zarezerwowany").Value),
                                                       UserName = k.Attribute("UserName").Value
                                                   })).ToList();
                                 
            return listaKsiazek;
        }
       

        public override void Write(List<Ksiazka> lista)
        {
            XDocument document = new XDocument();
            var ksiazki = new XElement("Ksiazki");
            foreach (var ksiazka in lista)
            {
                ksiazki.Add(KsiazkaToXml(ksiazka));
            }
                document.Add(ksiazki);
                document.Save(path);
        }
        public override void Add(Ksiazka ksiazka)
        {
            var listaKsiazek = Read(path);
            Dodawanie(ksiazka, listaKsiazek);
        }
        public override void Remove(int id)
        {
            var listaKsiazek = Read(path);
            Usuwanie(id, listaKsiazek);
        }
        public override void Update(Ksiazka ksiazka)
        {
            var listaKsiazek = Read(path);
            Updatowanie(ksiazka, listaKsiazek);
        }
        private static XElement KsiazkaToXml(Ksiazka ksiazka)
        {
            var ksiazkaXML = new XElement("ksiazka",
                          new XAttribute(nameof(Ksiazka.Id), ksiazka.Id),
                          new XAttribute(nameof(Ksiazka.Nazwa), ksiazka.Nazwa),
                          new XAttribute(nameof(Ksiazka.Kategoria), ksiazka.Kategoria),
                          new XAttribute(nameof(Ksiazka.Status.Dostepny), ksiazka.Status.Dostepny),
                          new XAttribute(nameof(Ksiazka.Status.Wypozyczony), ksiazka.Status.Wypozyczony),
                          new XAttribute(nameof(Ksiazka.Status.Zarezerwowany), ksiazka.Status.Zarezerwowany),
                          new XAttribute(nameof(Ksiazka.Status.UserName), ksiazka.Status.UserName));
            return ksiazkaXML;
        }
    }
}

                           