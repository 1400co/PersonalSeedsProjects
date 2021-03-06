﻿using Quartz;
using System;
using System.Threading.Tasks;

namespace ApplicationServiceJobs.Jobs
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
