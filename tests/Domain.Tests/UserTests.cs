using System;
using Xunit;
using Shouldly;

namespace Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void CreateAndCompareUser()
        {
            var user = new User("test");
            var user2 = new User("test");
            user.ShouldBe(user2);
        }
    }
}
