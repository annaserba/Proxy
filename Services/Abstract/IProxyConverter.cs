using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IProxyConverter
    {
        System.Threading.Tasks.Task<string> GetProxyAsync(Services.DTO.ProxyModel model);
    }
}
