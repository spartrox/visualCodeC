using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;

namespace Domain
{
    [Serializable]
    public sealed class CantinaException : Exception
    {
        public CantinaException(string message) : base(message) {   }
        protected CantinaException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
    
    public sealed class Cantina
    {
        public static readonly Time DEFAULT_BEGIN = new Time(11, 30);
        public static readonly Time DEFAULT_END = new Time(14, 30);
        
        private readonly HashSet<User> _reservedUsers = new HashSet<User>();
        private readonly SortedDictionary<(Time, User), Reservation> _reservations = new SortedDictionary<(Time, User), Reservation>(); 
        
        public Cantina(int maxUsers, Time begin, Time end)
        {
            if (maxUsers < 1)
                throw new ArgumentException("0 person makes no sense", nameof(maxUsers));
            if (begin >= end)
                throw new ArgumentException("Begin and end of the cantina are ... let's say hard to match");
            
            MaxUsers = maxUsers;
            Begin = begin;
            End = end;
        }
        
        public int MaxUsers { get; private init; }
        public Time Begin { get; private init; }
        public Time End { get; private init; }

        public void LoadReservations(IEnumerable<Reservation> reservations)
        {
            foreach (var reservation in reservations)
                this.AddReservation(reservation);
        }

        public void AddReservation(Reservation reservation)
        {
            _reservations.Add((reservation.TimeSlot.Begin, reservation.User), reservation);
            _reservedUsers.Add(reservation.User);
        }

        public User User { get; private init; }
        public TimeSlot TimeSlot { get; private init; }

        public IEnumerable<Reservation> ReadReservations() => _reservations.Values;

        public bool UserHasReservation(User user) => _reservedUsers.Contains(user);
    }
}