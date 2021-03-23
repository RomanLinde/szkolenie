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

    public class SzkolenieSerwis : Service
    {
        private static object _blokada = new object();
        private IDatabase _db;

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
                var zapytanie = c.From<Kontrahent>().Where(x => x.Id==id);
                var wynik = c.SqlList<Kontrahent>(zapytanie);
                return wynik.FirstOrDefault();
            }
        }
    }
}
