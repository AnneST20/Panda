using Panda.Models;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Panda.Scheduler.Jobs
{
    class RieltorJob : IJob
    {
        private ApplicationDbContext context;

        public RieltorJob(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
