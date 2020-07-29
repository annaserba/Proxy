using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IProxyConverter
    {
        string GetProxy(Services.DTO.ProxyModel model);
    }
}
