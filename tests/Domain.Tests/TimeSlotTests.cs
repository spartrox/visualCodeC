using System;
using System.IO;
using Shouldly;
using Xunit;

namespace Domain.Tests
{
    public class TimeSlotTests
    {
        [Fact]
        public void CreateTimeSlot()
        {
            var ts0 = new TimeSlot(11, 30, 30);
            var ts = new TimeSlot(11, 30, 30);

            ts.Begin.ShouldBe(new Time(11, 30));
            ts.End.ShouldBe(new Time(12, 0));
            ts.ShouldBe(ts0);

            var ts2 = new TimeSlot(14, 00, 30);

            ts2.Begin.ShouldBe(new Time(14, 00));
            ts2.End.ShouldBe(new Time(14, 30));

            Should.Throw<ArgumentException>(() => new TimeSlot(12, 45, 12, 30))
                .Message.ShouldBe("Begin time must be sooner than end time");

            Should.Throw<ArgumentException>(() => new TimeSlot(10, 10, 1))
                .Message.ShouldBe($"You cannot eat in less than {TimeSlot.MIN_DURATION.TotalMinutes} minutes");
            Should.Throw<ArgumentException>(() => new TimeSlot(10, 10, 31))
                .Message.ShouldBe($"You cannot eat in more than {TimeSlot.MAX_DURATION.TotalMinutes} minutes");
        }

        [Fact]
        public void TimeSlotIsBetween()
        {
            var ts = new TimeSlot(12, 30, 12, 45);
            ts.IsBetween(new Time(12, 30), new Time(12, 45)).ShouldBeTrue();
            ts.IsBetween(new Time(12, 29), new Time(12, 46)).ShouldBeTrue();
            ts.IsBetween(new Time(12, 31), new Time(12, 46)).ShouldBeFalse();
            ts.IsBetween(new Time(12, 29), new Time(12, 44)).ShouldBeFalse();
        }

        [Fact]
        public void TimeSlotCollide()
        {
            var ts = new TimeSlot(12, 30, 12, 45);
            ts.Collide(new Time(12, 30), new Time(12, 45)).ShouldBeTrue();
            ts.Collide(new TimeSlot(12, 15, 12, 25)).ShouldBeFalse();
            ts.Collide(new TimeSlot(12, 31, 12, 40)).ShouldBeTrue();
            ts.Collide(new TimeSlot(12, 45, 12, 55)).ShouldBeTrue();
            ts.Collide(new TimeSlot(12, 46, 12, 55)).ShouldBeFalse();
        }
    }
}