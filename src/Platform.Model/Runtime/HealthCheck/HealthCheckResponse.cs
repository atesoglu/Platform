using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Model.HealthCheck
{
    public class HealthCheckResponse
    {
        public string Overall { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public ICollection<HealthCheckEntry> Dependencies { get; set; }

        public HealthCheckResponse()
        {
            Dependencies = new List<HealthCheckEntry>();
        }
    }
}