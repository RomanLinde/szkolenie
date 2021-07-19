using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkolenie.Api;

namespace Szkolenie.Implementacja
{
    [Route("/kontrahent/dodaj")]
    public class KomendaKontrahentDodaj
    {
        public string Nazwa { get; set; }
        public int Numer { get; set; }
    }

    public class Towar
    {
        public string Id { get; set; }
        public string Nazwa { get; set; }
    }

    [Route("/towary/daj")]
    public class KomendaDajTowary
    {
        public string Filtr { get; set; }
    }

    [Route("/towary/zapisz")]
    public class KomendaTowaryZapisz
    {
        public string Id { get; set; }
        public string Nazwa { get; set; }
    }

    [Route("/towary/usun")]
    public class KomendaTowaryUsun
    {
        public string Id { get; set; }
    }

    public class SzkolenieSerwis : Service
    {
        private static object _blokada = new object();
        private IDatabase _db;
        private static List<Towar> _lista = new List<Towar>()
        {
                new Towar() { Id="1" , Nazwa = "Buty" },
                new Towar() { Id="2" , Nazwa = "Bidon" },
                new Towar() { Id="3" , Nazwa = "Czapka" },
                new Towar() { Id="4" , Nazwa = "Rower" },
                new Towar() { Id="5" , Nazwa = "Plecak" },
                new Towar() { Id="6" , Nazwa = "Kijki" },
        };

        public SzkolenieSerwis(IDatabase db)
        {
            _db = db;
        }

        public Kontrahent Post(KomendaKontrahentDodaj arg)
        {
            using (var c = _db.DajPolaczenie())
            {
                var id = _db.DajGen("new_generator");
                c.Save(new Kontrahent() { Id = id, Numer = arg.Numer, Nazwa = arg.Nazwa });
                var zapytanie = c.From<Kontrahent>().Where(x => x.Id == id);
                var wynik = c.SqlList<Kontrahent>(zapytanie);
                return wynik.FirstOrDefault();
            }
        }

        public Towar[] Any(KomendaDajTowary arg)
        {
            if (String.IsNullOrWhiteSpace(arg.Filtr))
                arg.Filtr = "";
            return _lista.Where(x => x.Nazwa.Contains(arg.Filtr)).ToArray();
        }

        public Towar Any(KomendaTowaryZapisz arg)
        {
            if (String.IsNullOrWhiteSpace(arg.Nazwa))
                return new Towar();

            if (String.IsNullOrWhiteSpace(arg.Id))
            {
                var tow = new Towar() { Id = Guid.NewGuid().ToString(), Nazwa = arg.Nazwa };
                _lista.Add(tow);
                return tow;
            }
            var t = _lista.FirstOrDefault(x => x.Id == arg.Id);
            if (t == null)
                return new Towar();
            t.Nazwa = arg.Nazwa;
            return t;
        }

        public void Any(KomendaTowaryUsun arg)
        {
            _lista.RemoveAll(x => x.Id == arg.Id);
        }
    }
}
