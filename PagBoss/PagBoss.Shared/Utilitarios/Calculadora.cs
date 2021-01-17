using System;
using System.Collections.Generic;
using System.Linq;

namespace PagBoss.Shared.Utilitarios
{
    public static class Calculadora
    {
        public static void Calcula(ICollection<DateTime> DatasBoss, 
                                   out int IntervaloMaior,
                                   out int IntervaloMenor,
                                   out int IntervaloFrequente,
                                   out decimal IntervaloMedia,
                                   out int QuantidadeRegistros,
                                   out DateTime? UltimaAparicao)
        {
            IntervaloMaior = IntervaloMenor = IntervaloFrequente = QuantidadeRegistros = 0;
            IntervaloMedia = 0m;
            UltimaAparicao = null;

            if(DatasBoss.Count > 0)
            {
                ICollection<int> diasEntre = new List<int>();
                UltimaAparicao = DatasBoss.Max();
                QuantidadeRegistros = DatasBoss.Count;

                DateTime[] arrayDatas = DatasBoss.OrderBy(h => h.Date).ToArray();
                int loop = arrayDatas.Length-1;

                if(loop > 0)
                {
                    for (int i = 0; i < loop; i++)
                    {
                        var data1 = arrayDatas[i];
                        var data2 = arrayDatas[i+1];
                        diasEntre.Add((data2-data1).Days);
                    }

                    IntervaloMenor = diasEntre.Min();
                    IntervaloMaior = diasEntre.Max();
                    IntervaloFrequente = diasEntre.GroupBy(i => i).OrderByDescending(g => g.Count()).Select(x => x.Key).FirstOrDefault();
                }
            }
        }
    }
}