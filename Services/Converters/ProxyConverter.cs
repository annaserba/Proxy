using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Services.Converters
{
    public class ProxyConverter : Services.Abstract.IProxyConverter
    {
        public string ReadToEnd(Services.DTO.ProxyModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Uri.ToString()))
            {
                return String.Empty;
            }
            string result;
            WebRequest request = WebRequest.Create(model.Uri);
            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }

    }
}
