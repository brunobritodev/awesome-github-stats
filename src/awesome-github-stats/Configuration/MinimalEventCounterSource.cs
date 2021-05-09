using System.Diagnostics;
using System.Diagnostics.Tracing;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AwesomeGithubStats.Api.Configuration
{
    [EventSource(Name = "AwesomeGithubStats.EventCounter.Minimal")]
    public sealed class MinimalEventCounterSource : EventSource
    {
        public static readonly MinimalEventCounterSource Log = new MinimalEventCounterSource();

        private EventCounter _requestCounter;

        private MinimalEventCounterSource() =>
            _requestCounter = new EventCounter("request-time", this)
            {
                DisplayName = "Request Processing Time",
                DisplayUnits = "ms"
            };

        public void Request(string url, float elapsedMilliseconds)
        {
            WriteEvent(1, url, elapsedMilliseconds);
            _requestCounter?.WriteMetric(elapsedMilliseconds);
        }

        protected override void Dispose(bool disposing)
        {
            _requestCounter?.Dispose();
            _requestCounter = null;

            base.Dispose(disposing);
        }
    }
    public class LogRequestTimeFilterAttribute : ActionFilterAttribute
    {
        readonly Stopwatch _stopwatch = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext context) => _stopwatch.Start();

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();

            MinimalEventCounterSource.Log.Request(
                context.HttpContext.Request.GetDisplayUrl(), _stopwatch.ElapsedMilliseconds);
        }
    }

}
