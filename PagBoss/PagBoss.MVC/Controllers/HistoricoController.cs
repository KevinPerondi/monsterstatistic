using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PagBoss.Domain.Entities;
using PagBoss.Domain.Repositories;
using PagBoss.Shared.Utilitarios;

namespace PagBoss.MVC.Controllers
{
    [Route("")]
    public class HistoricoController : Controller
    {
        private readonly IHistoricoRepository _repository;

        public HistoricoController(IHistoricoRepository repository)
        {
            _repository = repository;
        }

        // public IActionResult Index()
        // {
        //     ViewData["Dados"] = _repository.GetKilledYesterday();
        //     return View();
        // }

        public IActionResult Index()
        {
            var all = _repository.GetAll();
            var names = all.GroupBy(x => x.BossName).Select(z => z.First().BossName).ToList();

            ICollection<BossData> bossData = new List<BossData>();

            foreach (var name in names)
            {
                var dates = all.Where(x => x.BossName == name).Select(x => x.KilledDate).ToList();

                Calculadora.Calcula(dates,
                                    out int IntervaloMaior,
                                    out int IntervaloMenor,
                                    out int IntervaloFrequente,
                                    out decimal IntervaloMedia, 
                                    out int Registros, 
                                    out DateTime? LastSee);

                var bossD = new BossData(name,
                                        LastSee, 
                                        IntervaloMenor, 
                                        IntervaloMaior, 
                                        IntervaloMedia, 
                                        IntervaloFrequente, 
                                        Registros);
                bossData.Add(bossD);
            }

            ViewData["Dados"] = bossData;
            return View();
        }
    }
}