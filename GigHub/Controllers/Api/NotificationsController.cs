using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using AutoMapper;

using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _unitOfWork.Notifications.GetNewNotificationsFor(userId);

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _unitOfWork.UserNotifications.GetUserNotificationsFor(userId);

            foreach (var notification in notifications)
                notification.Read();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
