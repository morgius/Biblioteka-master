using Newtonsoft.Json;
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
    class Program
    {
        static void Main(string[] args)
        {
            Obslugatxt obslugatxt = new Obslugatxt();
            Obslugajson obslugajson = new Obslugajson();
            Obslugaxml obslugaxml = new Obslugaxml();
            Biblioteka biblioteka = new Biblioteka(obslugatxt,obslugajson,obslugaxml);
            //biblioteka.Write();
            biblioteka.WybierzAkcje();
        }
    }
}

            