using System.Linq;

using FluentAssertions;

using GigHub.Core.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GigHub.Tests.Domain.Models
{
    [TestClass]
    public class GigTests
    {
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCanceledToTrue()
        {
            var gig = new Gig();

            gig.Cancel();

            gig.IsCanceled.Should().BeTrue();
        }

        [TestMethod]
        public void Cancel_WhenCalled_EachAttendeeShouldHaveANotification()
        {
            var gig = new Gig();
            gig.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Name = "1" } });

            gig.Cancel();

            var attendees = gig.GetAttendees().ToList();
            attendees[0].UserNotifications.Count.Should().Be(1);
        }
    }
}
