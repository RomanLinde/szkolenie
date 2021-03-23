using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Szkolenie.Api
{
    public interface IDatabase
    {
        void Prepare();
        IDbConnection DajPolaczenie();
        int DajGen(string nazwa);
    }
}
