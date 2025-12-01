using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSD.Domain.ValueObjects
{
    public class Duration : ValueObject
    {
        public decimal Hours { get; private set; }
        public decimal OverTime { get; private set; }

        // Private constructor ensures immutability via factory methods or object initializer
        private Duration() { }

        public Duration(decimal hours, decimal overTime)
        {
            if (hours < 0 || overTime < 0)
            {
                throw new ArgumentException("Hours and OverTime must be non-negative.");
            }
            Hours = hours;
            OverTime = overTime;
        }

        public decimal TotalTime => Hours + OverTime;

        // Factory method to create a new Duration based on this one
        public Duration AddHours(decimal hoursToAdd)
        {
            return new Duration(this.Hours + hoursToAdd, this.OverTime);
        }

        // Method required for value equality comparison (see Base ValueObject below)
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hours;
            yield return OverTime;
        }
    }
}
