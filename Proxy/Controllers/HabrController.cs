using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proxy.Controllers
{
    public class HabrController : Controller
    {
        private readonly IProxyConverter _proxyConverter;
        public HabrController(IProxyConverter proxyConverter)
        {
            _proxyConverter = proxyConverter;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = new Services.DTO.ProxyModel() { Uri = new Uri("https://habr.com/") };
            var result = _proxyConverter.ReadToEnd(model);
            return View("Index", result);
        }
    }
}
