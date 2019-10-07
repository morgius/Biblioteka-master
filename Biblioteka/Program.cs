using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Biblioteka
{
    /*To, co zajeło mi najwiecej czasu i czego nie udalo mi sie zrobic to ustalic bezposrednia sciezke do plikow w aplikacji.
     * Probowalem rzeczy jak
     * string path = Path.Combine(Environment.CurrentDirectory,"danetxt.txt");
     * string path = AppDomain.CurrentDomain.BaseDirectory;
     *  string path = ConfigurationManager.AppSettings["filexml"];
     * jednak daja one dostep do folderu bin\debug, nie do wlasciwego folderu w ktorym sie one znajduja.
     * Aby program zadzialal trzeba podmienic stringi wejsciowe w klasach obsugatxt itd
     */ 
    class Program
    {
        static void Main(string[] args)
        {
            Obslugatxt obslugatxt = new Obslugatxt();
            Obslugajson obslugajson = new Obslugajson();
            Obslugaxml obslugaxml = new Obslugaxml();
            Biblioteka biblioteka = new Biblioteka(obslugatxt,obslugajson,obslugaxml);
            //blioteka.Write();
            biblioteka.Read();
            biblioteka.WybierzAkcje();
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //string path = Path.Combine(Environment.CurrentDirectory,"danetxt.txt");
            //Console.WriteLine(path);




            Console.ReadLine();
        }
    }
}

            