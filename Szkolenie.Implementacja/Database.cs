using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Szkolenie.Api;

namespace Szkolenie.Implementacja
{
    internal class Database : IDatabase
    {
        private OrmLiteConnectionFactory _factory;

        public Database()
        {
            OrmLiteConfig.DialectProvider = FirebirdDialect.Provider;
            _factory = new OrmLiteConnectionFactory(
                "User=SYSDBA;Password=masterkey;Database=c:\\temp\\szkolenie.fdb;DataSource=127.0.0.1;Dialect=3;charset=ISO8859_1;");
        }

        public int DajGen(string nazwa)
        {
            using (var c = DajPolaczenie())
            {
                var command = c.CreateCommand();
                command.CommandText = $"select gen_id({nazwa}, 1) from rdb$database";
                var reader = command.ExecuteReader();
                reader.Read();
                var nr = reader.GetInt32(0);
                return nr;
            }
            return 0;
        }

        public IDbConnection DajPolaczenie()
        {
            return _factory.OpenDbConnection();
        }

        public void Prepare()
        {
        }
    }
}
