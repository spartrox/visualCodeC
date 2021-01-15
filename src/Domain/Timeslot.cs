using System;
using System.IO;
using System.Text.Json.Serialization;

namespace Domain
{
    public class TimeSlot : IEquatable<TimeSlot>
    {
        public static readonly TimeSpan MIN_DURATION = TimeSpan.FromMinutes(5);
        public static readonly TimeSpan MAX_DURATION = TimeSpan.FromMinutes(30);
        
        public TimeSlot(int beginHours, int beginMinutes, int endHours, int endMinutes)
            : this(new Time(beginHours, beginMinutes), new Time(endHours, endMinutes))
        { }
        
        public TimeSlot(int beginHours, int beginMinutes, int durationInMinutes)
            : this(new Time(beginHours, beginMinutes), new Time(beginHours, beginMinutes) + durationInMinutes)
        { }
        
        [JsonConstructor]
        public TimeSlot(Time begin, Time end)
        {
            if (begin > end) throw new ArgumentException("Begin time must be sooner than end time");
            var duration = end - begin;
            
            if (duration < MIN_DURATION)
                throw new ArgumentException($"You cannot eat in less than {MIN_DURATION.TotalMinutes} minutes");
            if (duration > MAX_DURATION)
                throw new ArgumentException($"You cannot eat in more than {MAX_DURATION.TotalMinutes} minutes");

            End = end;
            Begin = begin;
        }

        public Time Begin { get; private init; }
        public Time End { get; private init; }

        public bool IsBetween(Time begin, Time end)
        {
            if (Begin < begin) return false;
            if (End > end) return false;
            return true;
        }

        public bool Collide(TimeSlot other) => Collide(other.Begin, other.End);
        public bool Collide(Time begin, Time end)
        {
            if (end < Begin) return false;
            if (begin > End) return false;
            return true;
        }

        public bool Equals(TimeSlot? other)
        {
            if (other == null) return false;
            return Begin == other.Begin && End == other.End;
        }
    }
}