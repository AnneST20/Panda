using Panda.Scheduler.Jobs;
using Quartz;
using Quartz.Impl;

namespace Panda.Scheduler.Schedulers
{
    class SimpleScheduler
    {
        public static void Start()
        {

            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler().Result;
            sched.Start();

            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .WithIdentity("simpleJob", "mainGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(2)
                .RepeatForever())
                .Build();



            sched.ScheduleJob(job, trigger);
        }
    }
}
