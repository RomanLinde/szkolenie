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

    public class Kontrahent : IKontrahent
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public int Numer { get; set; }
        public string Adres { get; set; }
        public string Miasto { get; set; }
    }    
}
