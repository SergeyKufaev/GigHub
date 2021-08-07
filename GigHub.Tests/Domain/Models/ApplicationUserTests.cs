using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using GigHub.Core.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GigHub.Tests.Domain.Models
{
    [TestClass]
    public class ApplicationUserTests
    {
        [TestMethod]
        public void Notify_WhenCalled_ShouldAddTheNotification()
        {
            var user = new ApplicationUser();
            var notification = Notification.GigCanceled(new Gig());

            user.Notify(notification);

            user.UserNotifications.Count.Should().Be(1);
            var userNotification = user.UserNotifications.First();
            userNotification.Notification.Should().Be(notification);
            userNotification.User.Should().Be(user);
        }
    }
}
