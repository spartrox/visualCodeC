using System;

namespace Domain
{
    public record User : IComparable<User>
    {
        public User(string id) => (Id) = (id);

        public string Id { get; init; }

        public int CompareTo(User? other) => Id.CompareTo(other?.Id);
    }
}