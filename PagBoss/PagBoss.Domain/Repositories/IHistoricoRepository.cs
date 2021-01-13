using System.Collections.Generic;
using PagBoss.Domain.Entities;

namespace PagBoss.Domain.Repositories
{
    public interface IHistoricoRepository
    {
        ICollection<Historico> GetKilledYesterday();
        ICollection<Historico> GetByName(string bossName);
        ICollection<Historico> GetAll();
    } 
}