using System;
using Shouldly;
using Xunit;

namespace Domain.Tests
{
    public class CantinaTests
    {
        [Fact]
        public void CreateCantina()
        {
            var cantina = new Cantina(10, new Time(12, 30), new Time(14, 30));
            cantina.MaxUsers.ShouldBe(10);
            cantina.Begin.ShouldBe(new Time(12, 30));
            cantina.End.ShouldBe(new Time(14, 30));

            Should.Throw<ArgumentException>(() => new Cantina(0, new Time(12, 30), new Time(14, 30)))
                .Message.ShouldBe("0 person makes no sense (Parameter 'maxUsers')");
            Should.Throw<ArgumentException>(() => new Cantina(10, new Time(12, 30), new Time(12, 30)))
                .Message.ShouldBe("Begin and end of the cantina are ... let's say hard to match");
            Should.Throw<ArgumentException>(() => new Cantina(10, new Time(12, 31), new Time(12, 30)))
                .Message.ShouldBe("Begin and end of the cantina are ... let's say hard to match");
        }

        private Cantina GetTestCantina(int maxUsers = 1)
        {
            var cantina = new Cantina(maxUsers, Cantina.DEFAULT_BEGIN, Cantina.DEFAULT_END);
            cantina.AddReservation(new Reservation(new User("user"), new TimeSlot(11,30,15)));
            return cantina;
        }
        
        [Fact]
        public void CantinaCanStoreMultipleReservationsToTheSamePeriod()
        {
            var cantina = GetTestCantina(2);
            cantina.AddReservation(new Reservation(new User("user2"), new TimeSlot(11,30,15)));
            // no exception
        }
        
        [Fact]
        public void CantinaCanStoreReservation()
        {
            var cantina = GetTestCantina();
            cantina.UserHasReservation(new User("user")).ShouldBeTrue();
            cantina.UserHasReservation(new User("not user")).ShouldBeFalse();
        }

        [Fact]
        public void CantinaCheckItsSchedule()
        {
            var cantina = GetTestCantina();
            Should.Throw<CantinaException>(() =>
                cantina.AddReservation(new Reservation(new User("doe"), new TimeSlot(11, 12, 13))))
                .Message.ShouldBe("Reservation not in schedule");
            
            Should.Throw<CantinaException>(() =>
                    cantina.AddReservation(new Reservation(new User("doe"), new TimeSlot(14,20, 30))))
                .Message.ShouldBe("Reservation not in schedule");
        }

        [Fact]
        public void CantinaCanDetectCollisions()
        {
            var cantina = GetTestCantina();
            Should.Throw<CantinaException>(() =>
                    cantina.AddReservation(new Reservation(new User("user"), new TimeSlot(14, 12, 10))))
                .Message.ShouldBe("User already reserved");
            
            cantina.AddReservation(new Reservation(new User("user2"), new TimeSlot(13,30,15)));
            
            Should.Throw<CantinaException>(() =>
                    cantina.AddReservation(new Reservation(new User("user3"), new TimeSlot(13, 30, 15))))
                .Message.ShouldBe("Too much people !");
            Should.Throw<CantinaException>(() =>
                    cantina.AddReservation(new Reservation(new User("user3"), new TimeSlot(13, 25, 15))))
                .Message.ShouldBe("Too much people !");
            Should.Throw<CantinaException>(() =>
                    cantina.AddReservation(new Reservation(new User("user3"), new TimeSlot(13, 40, 5))))
                .Message.ShouldBe("Too much people !");
            
            cantina.AddReservation(new Reservation(new User("user3"), new TimeSlot(14,0,5)));
        }
    }
}
