﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;

namespace Shiva.Tools
{
    [TestClass]
    public class UTStringTools
    {
        [TestMethod]
        public void TestFormatByName()
        {
            Assert.IsTrue("".FormatByName(new Dictionary<string, object>()) == "");
            Assert.IsTrue("test".FormatByName(new Dictionary<string, object>()) == "test");
            Assert.IsTrue("test".FormatByName(null) == "test");

            var formatvalue = "value1 {value1} value2 {value2:D}"
                .FormatByName(new Dictionary<string, object>
                {
                    { "value2",new DateTime(2018,1,1)},
                    { "value1","test"}
                }, CultureInfo.GetCultureInfo("fr"));

            Assert.IsTrue(formatvalue == "value1 test value2 lundi 1 janvier 2018", message: formatvalue);

            formatvalue = "value1 {value1} value2 {value2:D}"
               .FormatByName(new Dictionary<string, object>
               {
                    { "value2",new DateTime(2018,1,1)},
                    { "value1","test"}
               }, CultureInfo.GetCultureInfo("en"));

            Assert.IsTrue(formatvalue == "value1 test value2 Monday, January 1, 2018", message: formatvalue);
        }
    }
}
