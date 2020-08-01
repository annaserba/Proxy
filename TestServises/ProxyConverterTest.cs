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
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ProxyConverter()
        {
            var proxyConverter = new ProxyConverter();
            var result = proxyConverter.GetProxy(new Services.DTO.ProxyModel() { 
                FullOriginal = new Uri("https://www.microsoft.com/")
            });
            Assert.IsNotEmpty(result);
        }
    }
}