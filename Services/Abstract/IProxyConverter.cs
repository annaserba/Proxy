using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IProxyConverter
    {
        string ReadToEnd(Services.DTO.ProxyModel model);
    }
}
