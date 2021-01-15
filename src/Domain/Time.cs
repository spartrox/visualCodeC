using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace Domain
{
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        [JsonConstructor]
        public Time(int hours, int minutes)
        {
            if (hours < 0 || hours > 23) throw new ArgumentException("Hours have to be between 0 and 23");
            if (minutes < 0 || minutes > 59) throw new ArgumentException("Minutes have to be between 0 and 59");
            Hours = hours;
            Minutes = minutes;
        }

        public int Hours { get; private init; }
        public int Minutes { get; private init; }

        public int TotalMinutes() => Hours * 60 + Minutes;

        public static Time FromTotalMinutes(int minutes) => new Time(0,0) + minutes;
        
        public TimeSpan ToTimeSpan() => TimeSpan.FromMinutes(this.TotalMinutes());
        public static implicit operator TimeSpan(Time time) => time.ToTimeSpan();

        public override string ToString() => $"{this.Hours:00}:{this.Minutes:00}";

        /// <summary>
        /// Add minutes to the time
        /// </summary>
        public static Time operator +(Time time, int minutes)
        {
            var total = time.TotalMinutes() + minutes;
            var hours = total / 60;
            var mins = total % 60;
            return new Time(hours, mins);
        }

        /// <summary>
        /// Subtract minutes to the time
        /// </summary>
        public static Time operator -(Time time, int minutes)
        {
            var total = time.TotalMinutes() - minutes;
            var hours = total / 60;
            var mins = total % 60;
            return new Time(hours, mins);
        }

        /// <summary>
        /// Subtract time to a time, returning a TimeSpan
        /// </summary>
        public static TimeSpan operator -(Time upper, Time lower)
        {
            var up = upper.ToTimeSpan();
            var down = lower.ToTimeSpan();
            if (up < down)
                throw new InvalidOperationException("You cannot subtract a time greater than the first one");
            return up - down;
        }

        public override int GetHashCode() => HashCode.Combine(Hours, Minutes);
        public bool Equals(Time other) => Hours == other.Hours && Minutes == other.Minutes;
        public int CompareTo(Time other) => this.TotalMinutes() - other.TotalMinutes();

        public override bool Equals(object? obj) => obj is Time other && Equals(other);

        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);
        public static bool operator !=(Time t1, Time t2) => !t1.Equals(t2);

        public static bool operator <(Time t1, Time t2) => t1.TotalMinutes() < t2.TotalMinutes();
        public static bool operator >(Time t1, Time t2) => t1.TotalMinutes() > t2.TotalMinutes();
        public static bool operator <=(Time t1, Time t2) => t1.TotalMinutes() <= t2.TotalMinutes();
        public static bool operator >=(Time t1, Time t2) => t1.TotalMinutes() >= t2.TotalMinutes();
    }
}