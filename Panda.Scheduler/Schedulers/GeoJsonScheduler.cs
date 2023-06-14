using Panda.Jobs;
using Quartz;
using Quartz.Impl;

namespace Panda.Schedulers
{
    internal class GeoJsonScheduler
    {
        public static void Start()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler().Result;
            sched.Start();

            IJobDetail job = JobBuilder.Create<GeoJsonJob>()
                .WithIdentity("geoJsonJob", "mainGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(3)
                .RepeatForever())
                .Build();



            sched.ScheduleJob(job, trigger);
        }
    }
}
