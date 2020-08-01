﻿using System;
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
        public string GetProxy(Services.DTO.ProxyModel model)
        {
            return ReplaceLinks(ReadToEnd(model), model);
        }
        private string ReadToEnd(Services.DTO.ProxyModel model)
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
                response = request.GetResponse();
                stream = response.GetResponseStream();
                reader = new StreamReader(stream);
                result = reader.ReadToEnd();
            }
            catch (Exception e)
            {

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
                html = new Regex(pattern.Key).Replace(html, pattern.Value);
            }
            return html;
        }
    }
}
