using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using Szkolenie.Api;

namespace Szkolenie.Implementacja
{
    [Route("/nazwa/daj")]
    public class KomendaDajNazwe
    {
        public string Nazwa { get; set; }
    }

    [Route("/nazwa/daj1")]
    public class KomendaDajNazwe1
    {
        public string Id { get; set; }
    }

    public class SzkolenieSerwis : Service
    {
        private int nr = 1;
        private IKontrahent _kontrahent;

        public SzkolenieSerwis(IKontrahent kontrahent)
        {
            _kontrahent = kontrahent;
        }

        public SzkolenieSerwis()
        {
            Console.WriteLine("Utworzono");
        }

        public string Get(KomendaDajNazwe arg)
        {
            _kontrahent.Numer++;
            //var k = ResolveService<IKontrahent>();
            //k.Numer++;
            return $"OK: {arg.Nazwa} {_kontrahent.Numer}";
        }

        public string Get(KomendaDajNazwe1 arg)
        {
            return "OK:" + arg.Id;
        }
    }
}
