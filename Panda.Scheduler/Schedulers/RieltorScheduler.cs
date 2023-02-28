using Panda.Scheduler.Jobs;
using Quartz;
using Quartz.Impl;

namespace Panda.Scheduler.Schedulers
{
    class RieltorScheduler
    {

        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<RieltorJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    //s.WithIntervalInSeconds(20)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
