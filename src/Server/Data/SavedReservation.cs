using System;

namespace Server.Data
{
    public class SavedReservation
    {
        public string? User { get; set; }
        public DateTime Date { get; set; }
        public int BeginMinute { get; set; }
        public int EndMinute { get; set; }
    }
}