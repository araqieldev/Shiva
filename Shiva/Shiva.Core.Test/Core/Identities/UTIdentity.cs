﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using System.Collections.Generic;

namespace Shiva.Core.Identities
{
    [TestClass]
    public class UTIdentity:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitialize()
        {
            var id = new Identity("Shiva.Core.Identities.Test");
            Assert.IsTrue(id == "Shiva.Core.Identities.Test");
            Assert.IsTrue(id.Key == "Test");
            Assert.IsTrue(id.Namespace == "Shiva.Core.Identities");

            id = new Identity(" Shiva . Core . Identities . Test ");
            Assert.IsTrue(id == "Shiva.Core.Identities.Test");
            Assert.IsTrue(id.Key == "Test");
            Assert.IsTrue(id.Namespace == "Shiva.Core.Identities");

            id = new Identity("Shiva.Core.Identities","Test");
            Assert.IsTrue(id == "Shiva.Core.Identities.Test");
            Assert.IsTrue(id.Key == "Test");
            Assert.IsTrue(id.Namespace == "Shiva.Core.Identities");

            id = new Identity("test");
            Assert.IsTrue(id == "test");
            Assert.IsTrue(id.Key == "test");
            Assert.IsTrue(id.Namespace == "");
            Assert.IsTrue(id.Namespace == Namespace.Null);
            
        }

        [TestMethod]
        public void FailInitialize()
        {
            Action<string> ctor1 = x => new Identity(x);
            Action<Namespace,string> ctor2 = (x,y)=> new Identity(x, y);

            ctor1.Invoking(x => x(null)).Should().Throw<ArgumentNullException>();
            ctor2.Invoking(x => x(null, "test")).Should().NotThrow();
            ctor2.Invoking(x => x("test", null)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestOperator()
        {
            var id = new Identity("Shiva.Core.Identities.Test");
            Assert.IsTrue(id == "Shiva.Core.Identities.Test");
            Assert.IsTrue("Shiva.Core.Identities.Test" == id);

            var l = new List<string>();
            l.Invoking(x => x.Add(id)).Should().NotThrow();

            var l2 = new List<Identity>();
            l.Invoking(x => x.Add("test")).Should().NotThrow();
        }
    }
}
