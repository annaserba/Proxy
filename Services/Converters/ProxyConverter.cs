using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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
            return ReplaceLinks(await ReadToEndAsync(model).ConfigureAwait(false), model);
        }
        private async System.Threading.Tasks.Task<string> ReadToEndAsync(Services.DTO.ProxyModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.FullOriginal.ToString()))
            {
                return String.Empty;
            }
            string result= String.Empty;
            WebRequest request = WebRequest.Create(model.FullOriginal);
            request.Method = "GET";
            WebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                using (response = await request.GetResponseAsync().ConfigureAwait(false))
                {
                    stream = response.GetResponseStream();
                    reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                _logger.LogDebug(1, e.Message);
            }
            finally
            {
                stream?.Close();
                reader?.Close();
                response?.Close();
            }
            return result;
        }
        private string ReplaceLinks(string html, Services.DTO.ProxyModel model)
        {
            foreach (var pattern in model.ReplacePatterns)
            {
                html = new Regex(pattern.Key, RegexOptions.Multiline| RegexOptions.IgnoreCase).Replace(html, pattern.Value);
            }
            return html;
        }
    }
}
