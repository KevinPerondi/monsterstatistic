using System;
using System.Data;
using Npgsql;
using PagBoss.Shared;

namespace PagBoss.Infra.DataContexts
{
    public class PagBossDataContext : IDisposable
    {
        public IDbConnection sqlCon { get; set; }

        public PagBossDataContext()
        {
            this.sqlCon = new NpgsqlConnection(Settings.ConnectionString);
            sqlCon.Open();
        }

        public void Dispose()
        {
            if(sqlCon.State != ConnectionState.Closed)
                sqlCon.Close();
        }
    }
}