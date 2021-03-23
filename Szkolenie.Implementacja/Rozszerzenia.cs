using System;
using Szkolenie.Api;

namespace Szkolenie.Implementacja
{
    public static class Rozszerzenia
    {
        public static string DajAdres(this IKontrahent k)
        {
            return k.Adres + " " + k.Miasto;
        }
    }
}
