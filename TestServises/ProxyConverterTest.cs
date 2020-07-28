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
            var result = proxyConverter.ReadToEnd(new Services.DTO.ProxyModel() { Uri = new System.Uri("https://www.microsoft.com/") });
            Assert.IsNotEmpty(result);
        }
    }
}