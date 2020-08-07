using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System.Text.RegularExpressions;

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
        public async Task<IActionResult> IndexAsync(string query)
        {
            var model = new Services.DTO.ProxyModel()
            {
                FullOriginal = new Uri("https://habr.com/" + query),
                AddString = "™"
            };

            model.ReplacePatterns.Add($"//{ model.FullOriginal.Host }",
                $"//{ HttpContext.Request.Host }");
            model.ReplacePatterns.Add(@"\\/\\/" + model.FullOriginal.Host,
                @"\/\/" + HttpContext.Request.Host);
            var result = await _proxyConverter.GetProxyAsync(model).ConfigureAwait(false);
            return View("Index", result);
        }
    }
}
