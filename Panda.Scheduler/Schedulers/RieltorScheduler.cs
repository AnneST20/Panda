using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System;
using Serilog;
using Panda.Jobs;
using Panda.Models;

namespace Panda.Schedulers
{
    public class RieltorScheduler
    {

        public static void Start()
        {

            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler().Result;
            sched.Start();

            IJobDetail job = JobBuilder.Create<RieltorJob>()
                .WithIdentity("rieltorJob", "mainGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInHours(24)
                .RepeatForever())
                .Build();

            sched.ScheduleJob(job, trigger); 
        }

        //public static async void Start()
        //{
        //    Log.Information("RieltorScheduler started");    

        //    IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        //    await scheduler.Start();

        //    IJobDetail job = JobBuilder.Create<RieltorJob>().Build();

        //    ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
        //        .WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
        //        .StartNow()                            // запуск сразу после начала выполнения
        //        .WithSimpleSchedule(x => x            // настраиваем выполнение действия
        //            .WithIntervalInMinutes(10)          // через 1 минуту
        //            .RepeatForever())                   // бесконечное повторение
        //        .Build();                               // создаем триггер

        //    await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы

        //    Log.Information("RieltorScheduler setup completed");
        //}
    }
}
