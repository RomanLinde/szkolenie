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
            var cors = new CorsFeature(
            allowedOrigins: "*",
            allowedMethods: "GET, POST, PUT, DELETE, OPTIONS",
            allowedHeaders: "Content-Type",
            allowCredentials: true);
            Plugins.Add(cors);

            container.RegisterAutoWiredAs<Kontrahent, IKontrahent>().ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWiredAs<Database, IDatabase>().ReusedWithin(ReuseScope.Container);
        }
    }
}
