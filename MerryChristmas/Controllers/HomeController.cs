using MerryChristmas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MerryChristmas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        List<int> randomArray = null;
        [HttpPost]
        public IActionResult Match(string nameList)
        {
            if (!string.IsNullOrEmpty(nameList))
            {
                randomArray = new List<int>();
                Dictionary<string, string> ss = new Dictionary<string, string>();

                List<string> arrayName = nameList.Split('\n').ToList();

                arrayName.RemoveAll(w => string.Empty == w);

                foreach (var item in arrayName.ToList())
                {
                    var sameNames = arrayName.Where(w => w.Equals(item)).ToList();
                    if (sameNames.Count > 1)
                    {
                        sameNames.ForEach(name =>
                        {
                            int index = arrayName.IndexOf(name);
                            arrayName[index] = string.Format($"Listede {arrayName.IndexOf(name) + 1}. Sırada ki {arrayName[index]}");

                        });
                    }

                }
                if (arrayName.Count % 2 != 0)
                {
                    ViewBag.isShow = false;
                    ViewBag.errorMessage = string.Format($"Hadi diyelim eşledim sence {arrayName.Count} kişi eşit dağıtılır mı bir kişi açıkta mı kalır? cevabını dinliyorum microfonunu aktif ettim şu an.");

                }
                else
                {
                    ViewBag.isShow = true;
                }

                if (ViewBag.isShow == true)
                {
                    while (ss.Count != arrayName.Count)
                    {
                        int firstForNumber = RandomNumber(arrayName.Count);
                        string first = arrayName[firstForNumber];
                        int secondForNumber = RandomNumber(arrayName.Count);
                        string second = arrayName[secondForNumber];
                        if (!ss.ContainsKey(first) && !first.Equals(second))
                        {
                            if (!ss.ContainsValue(second))
                                ss.Add(first, second);
                            else
                                continue;

                        }

                    }

                }

                ViewBag.Model = ss;
            }
            return View("Index");
        }
        private int RandomNumber(int arrayLength)
        {
            Random random = new Random();

            while (true)
            {
                int rndNumber = random.Next(0, arrayLength);
                if (randomArray.IndexOf(rndNumber) < 0)
                    return rndNumber;
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
