using System;

namespace Platform.Model.HealthCheck
{
    public class HealthCheckEntry
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public TimeSpan Duration { get; set; }

        public HealthCheckEntry(string name, string status, TimeSpan duration)
        {
            Name = name;
            Status = status;
            Duration = duration;
        }
    }
}