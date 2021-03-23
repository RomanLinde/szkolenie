using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Szkolenie.Api;
using Szkolenie.Implementacja;
using Szkolenie.Pomoc;

namespace Szkolenie
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var host = new SzkolenieHost();
            var h = WebHost
                    .CreateDefaultBuilder(args)
                    //.UseUrls(_konfiguracja.AdresSerwisu)
                    .UseStartup<Startup>();
            var b = h.Build();
            b.Run();
        }

        
    }
}
