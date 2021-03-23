using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Szkolenie.Api;

namespace Szkolenie.Implementacja
{
    public class SzkolenieHost : AppHostBase
    {
        public SzkolenieHost() : base("", Assembly.GetExecutingAssembly())
        {

        }

        public override void Configure(Container container)
        {
            container.RegisterAutoWiredAs<Kontrahent, IKontrahent>().ReusedWithin(ReuseScope.Request);
        }
    }
}
