﻿using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct User
    {
        public readonly string Email;

        //Logger
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        internal User(string email)
        {
            this.Email = email;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            log.Info("Logger test!");
        }
    }
}
