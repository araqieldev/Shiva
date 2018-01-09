﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using Shiva.Core.Services;
using Shiva.Services;
using Shiva.Exceptions;

namespace Shiva.Core.Ioc
{
    [TestClass]
    public class UTServiceContainerBase:BaseTest
    {
        [TestMethod]
        public void TestInitialization()
        {
            var mock = new Mock<ServiceContainerBase>(this.LogManager);
            var instance = mock.Object;
        }

        [TestMethod]
        public void TestRegisterType()
        {
            var mock = new Mock<ServiceContainerBase>(this.LogManager);
            var instance = mock.Object;

            instance.Register<ILogManager>(() => this.LogManager, ScopeServiceEnum.Singleton);
            instance.Register<ILogManager, Log4NetLogManager>();
        }
    }
}
