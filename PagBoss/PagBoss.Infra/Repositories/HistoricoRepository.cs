using System.Collections.Generic;
using System.Linq;
using Dapper;
using PagBoss.Domain.Entities;
using PagBoss.Domain.Repositories;
using PagBoss.Infra.DataContexts;

namespace PagBoss.Infra.Repositories
{
    public class HistoricoRepository : IHistoricoRepository
    {
        private readonly PagBossDataContext _context;

        public HistoricoRepository(PagBossDataContext context)
        {
            _context = context;
        }

        public ICollection<Historico> GetAll()
        {
            return _context.sqlCon.Query<Historico>("select * from historico order by Id desc").ToList();
        }

        public ICollection<Historico> GetByName(string bossName)
        {
            bossName = bossName.Replace("'","");
            return _context.sqlCon.Query<Historico>($"select * from historico where bossname like '%{bossName}%'").ToList();
        }

        public ICollection<Historico> GetKilledYesterday()
        {
            return _context.sqlCon.Query<Historico>($"select * from historico where killeddate in (select max(killeddate) from historico)").ToList();
        }
    }

}