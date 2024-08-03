using FluentScheduler;
using OnyxScheduling.Interfaces;

namespace OnyxScheduling.Controllers;

public class SeedDataService: Registry
{
    
    public SeedDataService()
    {
        Schedule<DailyTask>().ToRunEvery(1).Days();
    }
}

public class DailyTask : IJob
{
    
    public void Execute()
    {
        // call the method to run weekly here
    }
}