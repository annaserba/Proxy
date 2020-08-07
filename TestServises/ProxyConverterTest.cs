using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Services.Abstract;
using Services.Converters;
using Services.DTO;
using System;

namespace TestServises
{
    public class ProxyConverterTest
    {
        [Test]
        public void ProxyConverter()
        {
            var loggerMock = new Mock<ILogger<ProxyConverter>>();
            var proxyConverter = new ProxyConverter(loggerMock.Object);
            var result = proxyConverter.GetProxyAsync(new Services.DTO.ProxyModel() { 
                FullOriginal = new Uri("https://www.microsoft.com/")
            });
            Assert.IsNotEmpty(result.Result);
        }
    }
}