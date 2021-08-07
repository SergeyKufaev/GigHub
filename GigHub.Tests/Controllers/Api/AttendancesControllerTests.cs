
using System.Web.Http.Results;

using FluentAssertions;

using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _repository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new Mock<IAttendanceRepository>();

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Attendances).Returns(_repository.Object);

            _controller = new AttendancesController(unitOfWork.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_AttendanceAlreadyExists_ShouldReturnBadRequest()
        {
            _repository.Setup(r => r.GetAttendance(1, _userId)).Returns(new Attendance());

            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttendance_NoAttendanceWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.DeleteAttendance(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnOk()
        {
            _repository.Setup(r => r.GetAttendance(1, _userId)).Returns(new Attendance());

            var result = _controller.DeleteAttendance(1);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnTheIdOfDeletedAttendance()
        {
            _repository.Setup(r => r.GetAttendance(1, _userId)).Returns(new Attendance());

            var result = (OkNegotiatedContentResult<int>)_controller.DeleteAttendance(1);

            result.Content.Should().Be(1);
        }
    }
}
