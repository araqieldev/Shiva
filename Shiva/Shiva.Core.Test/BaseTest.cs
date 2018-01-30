﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shiva.Core.Services;
using log4net;
using System.IO;
using Shiva.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shiva
{
    [TestClass]
    public abstract class BaseTest
    {
        private static ILogManager _logm;
        private TestContext _testContextInstance;        
        private const string _separatorTest = "*****************************************************************************************************************";
        private const string _separatorClass = "----------------------------------------------------------------------------------------------------------------";

        static BaseTest()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Log.xml"));
            _logm = new Log4NetLogManager();
        }

        public TestContext TestContext
        {
            get
            {
                return this._testContextInstance;
            }
            set
            {
                this._testContextInstance = value;
            }
        }

        public ILogManager LogManager => _logm;
                
        public static void ClassInit(TestContext context)
        {
            var _logger = _logm.CreateLogger(context.FullyQualifiedTestClassName);
            if (_logger.DebugIsEnabled)
            {
                _logger.Debug(_separatorTest);
                _logger.Debug(_separatorClass);
                _logger.Debug($" Initialisation of {context.FullyQualifiedTestClassName} class Test.");
                _logger.Debug(_separatorClass);
                _logger.Debug(_separatorTest);
            }
        }

        [TestInitialize]
        public void InitTest()
        {
            var _logger = _logm.CreateLogger(this.GetType());
            if (_logger.DebugIsEnabled)
            {
                _logger.Debug(_separatorTest);             
                _logger.Debug($" Initialisation of {this.TestContext.TestName} Test.");
                _logger.Debug($" DeployDirectory: {this.TestContext.DeploymentDirectory} TestDir{this.TestContext.TestDir}.");                
                _logger.Debug(_separatorTest);
             
            }
        }
    }
}