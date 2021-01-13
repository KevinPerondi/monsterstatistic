using System;
using PagBoss.Shared.Entities;

namespace PagBoss.Domain.Entities
{
    public class Historico : Entity
    {
        public string BossName { get; set; }
        public DateTime KilledDate { get; set; }
        public DateTime InsertionDate { get; set; }
        public string Server { get; set; }
    }

}