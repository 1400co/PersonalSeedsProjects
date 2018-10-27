using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApplicationServiceJobs.Jobs;

namespace ApplicationServiceJobs
{
    class Program
    {
        #region Members
        private static string _Group = "Group1";
        #endregion


        static void Main(string[] args)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            RunProgramRunExample().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();

        }

        private static void ConfigureJobs(IScheduler scheduler)
        {
            SetJob<HelloJob>(scheduler, "JobFlowRunner", _Group, 1);
            SetJob<HelloJob2>(scheduler, "JobFlowRunner2", _Group, 1);
        }

        private static async Task RunProgramRunExample()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                ConfigureJobs(scheduler);

            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        private static void SetJob<T>(IScheduler scheduler, string namePrefix, string groupName, int withIntervalInMinutes) where T : IJob
        {
            if (withIntervalInMinutes == 0)
                return;

            // define the job and tie it to our HelloJob class
            var runnerJob = JobBuilder.Create<T>()
                .WithIdentity($"{namePrefix}_Job", groupName)
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            var runnerTrigger = TriggerBuilder.Create()
                .WithIdentity($"{namePrefix}_Schedule", groupName)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(withIntervalInMinutes)
                    .RepeatForever()).Build();

            scheduler.ScheduleJob(runnerJob, runnerTrigger);
            var nextFireTime = runnerTrigger.GetNextFireTimeUtc();

            if (nextFireTime != null)
                Console.WriteLine($"Next {namePrefix} RunnerJob Fire Time: {nextFireTime.Value}");
        }

    }


}



