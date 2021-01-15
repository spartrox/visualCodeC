namespace Domain
{
    public class Reservation
    {
        public Reservation(User user, TimeSlot timeSlot)
        {
            User = user;
            TimeSlot = timeSlot;
        }

        public User User { get; private init; }
        public TimeSlot TimeSlot { get; private init; }
    }
}