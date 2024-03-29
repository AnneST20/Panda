﻿using Panda.Jobs;
using Quartz;
using Quartz.Impl;
using System;

namespace Panda.Schedulers
{
    public class GeoJsonScheduler
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
                .StartAt(DateTime.Now.AddMinutes(10))
                .WithSimpleSchedule(x => x
                .WithIntervalInHours(24)
                .RepeatForever())
                .Build();



            sched.ScheduleJob(job, trigger);
        }
    }
}
