using System;
using System.IO;
using Shouldly;
using Xunit;

namespace Domain.Tests
{
    public class TimeTests
    {
        [Fact]
        public void CreateTimes()
        {
            Should.Throw<ArgumentException>(() => new Time(-1, 1))
                .Message.ShouldBe("Hours have to be between 0 and 23");
            Should.Throw<ArgumentException>(() => new Time(1, -1))
                .Message.ShouldBe("Minutes have to be between 0 and 59");
            
            var t1 = new Time();
            t1.Hours.ShouldBe(0);
            t1.Minutes.ShouldBe(0);
            
            var t2 = new Time(1, 2);
            t2.Hours.ShouldBe(1);
            t2.Minutes.ShouldBe(2);
            t2.TotalMinutes().ShouldBe(62);
            t2.ShouldBe(Time.FromTotalMinutes(62));
            
            var t3 = t2 + 15;
            t3.Hours.ShouldBe(1);
            t3.Minutes.ShouldBe(17);
            t3.TotalMinutes().ShouldBe(77);

            var t4 = t3 + 53;
            t4.Hours.ShouldBe(2);
            t4.Minutes.ShouldBe(10);
            t4.TotalMinutes().ShouldBe(130);
            t4.ShouldBe(Time.FromTotalMinutes(t4.TotalMinutes()));

            var t5 = t3 + 25 + 28;
            t5.ShouldBe(t4);
        }

        [Fact]
        public void TimesArithmetics()
        {
            var t0= new Time(12, 12);
            var t1 = new Time(12, 12);
            var t2 = new Time(13, 13);

            (t1 != t2).ShouldBeTrue();
            (t2 - t1).ShouldBe(TimeSpan.FromMinutes(61));
            ((t1 + 61) == t2 ).ShouldBeTrue();
            
            Should.Throw<InvalidOperationException>(()=> t1-t2)
                .Message.ShouldBe("You cannot subtract a time greater than the first one");

            (new Time(0, 10) - 10 == default(Time)).ShouldBeTrue();
            Should.Throw<ArgumentException>(() => new Time(0, 10) - 11)
                .Message.ShouldBe("Minutes have to be between 0 and 59");
            
            (t1 < t2).ShouldBeTrue();
            (t1 > t2).ShouldBeFalse();
            (t1 <= t2).ShouldBeTrue();
            (t1 >= t2).ShouldBeFalse();
            
            (t0 < t1).ShouldBeFalse();
            (t0 > t1).ShouldBeFalse();
            (t0 <= t1).ShouldBeTrue();
            (t0 >= t1).ShouldBeTrue();
            
            (t2 < t1).ShouldBeFalse();
            (t2 > t1).ShouldBeTrue();
            (t2 <= t1).ShouldBeFalse();
            (t2 >= t1).ShouldBeTrue();
        }
    }
}