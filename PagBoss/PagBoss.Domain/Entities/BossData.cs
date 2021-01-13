using System;

namespace PagBoss.Domain.Entities
{
    public class BossData
    {
        public BossData(string bossName, DateTime lastApparition, int minInterval, int maxInterval, decimal avarageInterval, int frequencyInterval, int registersNumber)
        {
            BossName = bossName;
            LastApparition = lastApparition;
            MinInterval = minInterval;
            MaxInterval = maxInterval;
            AvarageInterval = avarageInterval;
            FrequencyInterval = frequencyInterval;
            RegistersNumber = registersNumber;
        }

        public string BossName { get; set; }
        public DateTime LastApparition { get; set; }
        public int MinInterval { get; set; }
        public int MaxInterval { get; set; }
        public decimal AvarageInterval { get; set; }
        public int FrequencyInterval { get; set; }
        public int RegistersNumber { get; set; }
        public string MinIntervalDate 
        {
            get 
            {
                return LastApparition.AddDays(MinInterval).ToString("dd/MM/yyyy");
            }
        }public string MaxIntervalDate 
        {
            get 
            {
                return LastApparition.AddDays(MaxInterval).ToString("dd/MM/yyyy");
            }
        }
        public string FrequencyIntervalDate 
        {
            get 
            {
                return LastApparition.AddDays(FrequencyInterval).ToString("dd/MM/yyyy");
            }
        }

        public string AvarageIntervalDate 
        {
            get 
            {
                return LastApparition.AddDays((int) Math.Round(AvarageInterval)).ToString("dd/MM/yyyy");
            }
        }
    }
}