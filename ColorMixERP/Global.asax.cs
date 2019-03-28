using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using ColorMixERP.ScheduledJobs;

namespace ColorMixERP
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IScheduler _scheduler;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SetUpQuarztJob();
        }

        public void Application_End()
        {
            // Stop scheduler
            _scheduler.Shutdown(true);
        }

        private static void SetUpQuarztJob()
        {
            var minBeforeEnd = DateTime.Now.AddDays(1).Date.AddMinutes(-15);
            var factory = new StdSchedulerFactory();

            _scheduler = factory.GetScheduler().Result;
            _scheduler.Start();

            var job = JobBuilder.Create<BalanceAuditJob>()
                .WithIdentity("BalanceAuditJob", "BalanceAuditJobGroup")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("BalanceAuditTrigger", "BalanceAuditTriggerGroup")
                .StartAt(minBeforeEnd)
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }
    }
}