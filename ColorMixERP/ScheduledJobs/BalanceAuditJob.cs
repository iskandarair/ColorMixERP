using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Logging;
using Quartz;

namespace ColorMixERP.ScheduledJobs
{
    public class BalanceAuditJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                try
                {
                    new DailyBalanceBL().AuditDailyBalance();
                    LogManager.Instance.Info("Daily Balance Audit successfully performed");
                }
                catch (Exception ex)
                {
                    LogManager.Instance.Error("Error Occured while doing Daily Balance Audit CRON Job: " + Environment.NewLine + ex.Message
                                   + Environment.NewLine + ex.StackTrace );
                }
            });
        }
    }
}