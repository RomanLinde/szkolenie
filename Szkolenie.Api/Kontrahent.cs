using ServiceStack.DataAnnotations;
using System;

namespace Szkolenie.Api
{
    public interface IKontrahent
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public int Numer { get; set; }
        public string Adres { get; set; }
        public string Miasto { get; set; }
    }

    [Alias("PlayerProfile")]
    public class Kontrahent : IKontrahent
    {
        public int Id { get; set; }
        [Alias("NazwaKontrah")]
        public string Nazwa { get; set; }
        public int Numer { get; set; }
        public string Adres { get; set; }
        public string Miasto { get; set; }
    }    
}
