using System.Diagnostics;
using System.Text;
using Copmany.MVC.PL.Models;
using Copmany.MVC.PL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Copmany.MVC.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService01;
        private readonly IScopedService scopedService02;
        private readonly ITransentService transentService01;
        private readonly ITransentService transentService02;
        private readonly ISengletonSerivce sengletonSerivce01;
        private readonly ISengletonSerivce sengletonSerivce02;

        public HomeController(
            ILogger<HomeController> logger,
            IScopedService scopedService01,
            IScopedService scopedService02,
            ITransentService transentService01,
            ITransentService transentService02,
            ISengletonSerivce sengletonSerivce01,
            ISengletonSerivce sengletonSerivce02

            )
        {
            _logger = logger;
            this.scopedService01 = scopedService01;
            this.scopedService02 = scopedService02;
            this.transentService01 = transentService01;
            this.transentService02 = transentService02;
            this.sengletonSerivce01 = sengletonSerivce01;
            this.sengletonSerivce02 = sengletonSerivce02;
        }

        public string TestLifTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scopedService01 :: {scopedService01.GetGuid()}\n");
            builder.Append($"scopedService02 :: {scopedService02.GetGuid()}\n\n");
            builder.Append($"transentService01 :: {transentService01.GetGuid()}\n");
            builder.Append($"transentService02 :: {transentService02.GetGuid()}\n\n");
            builder.Append($"sengletonSerivce01 :: {sengletonSerivce01.GetGuid()}\n");
            builder.Append($"sengletonSerivce02 :: {sengletonSerivce02.GetGuid()}\n\n");

            return builder.ToString();
        }
        public IActionResult Index()
        {
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
