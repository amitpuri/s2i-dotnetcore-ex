using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app.Models;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        public Double FibonacciSeries(Double n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1; 
            return FibonacciSeries(n - 1) + FibonacciSeries(n - 2);
        }
        
        private IEnumerable<double> GetFibonacciSeries(double Nth)
        {

            Double length = Convert.ToDouble(Nth);
            for (double i = 0; i < length; i++)
            {
                yield return FibonacciSeries(i);
            }
        }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            foreach (var item in GetFibonacciSeries(double.MaxValue).AsParallel())
            {
                _logger.LogInformation(item);
            }
            return View();
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
