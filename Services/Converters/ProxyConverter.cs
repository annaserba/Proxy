
using AngleSharp;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.Converters
{
    public class ProxyConverter : Services.Abstract.IProxyConverter
    {
        private readonly ILogger<ProxyConverter> _logger;
        public ProxyConverter(ILogger<ProxyConverter> logger)
        {
            _logger = logger;
        }
        public async System.Threading.Tasks.Task<string> GetProxyAsync(Services.DTO.ProxyModel model)
        {
            return ApplyReplacePatterns(await ReadToEndAsync(model).ConfigureAwait(false), model);
        }
        private async System.Threading.Tasks.Task<string> ReadToEndAsync(Services.DTO.ProxyModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.FullOriginal.ToString()))
            {
                return String.Empty;
            }
            string result = String.Empty;
            try
            {
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(model?.FullOriginal.ToString()).ConfigureAwait(false);
                
                result = AddString(model.AddString, document);
            }
            catch (Exception e)
            {
                _logger.LogDebug(1, e.Message);
            }
            return result.Trim();
        }
        private string AddString(string addString, AngleSharp.Dom.IDocument document)
        {
            if (!string.IsNullOrEmpty(addString)
                && (document.ContentType=="text/html"|| document.ContentType == "application/json"))
            {
                var cellsLinq = document.All.Where(c => c.TagName != "SCRIPT" && c.TagName != "STYLE"&& c.TagName != "SVG"
                && c.ChildElementCount == 0 && !string.IsNullOrWhiteSpace(c.TextContent));
                foreach (var cell in cellsLinq)
                {
                    cell.TextContent = AddSymbolEndWorldInText(cell.TextContent, addString);
                }
            }
            return document.DocumentElement.OuterHtml;
        }
        private string AddSymbolEndWorldInText(string text, string addText)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            Regex regex = new Regex(@"\b\w{6}\b", RegexOptions.Multiline);
            return regex.Replace(text, $"$&{addText}");
        }
        private string ApplyReplacePatterns(string html, Services.DTO.ProxyModel model)
        {
            foreach (var pattern in model.ReplacePatterns)
            {
                html = new Regex(pattern.Key, RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(html, pattern.Value);
            }
            return html;
        }
    }
}
