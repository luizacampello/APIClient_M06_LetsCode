using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Ativ01Controller.Filters
{
    public class TimeResourceFilter : IResourceFilter
    {
        Stopwatch timer = new();

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            timer.Stop();
            Console.WriteLine($"Tempo de execução = {timer.ElapsedMilliseconds} ms");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            timer.Start();
        }
    }
}
