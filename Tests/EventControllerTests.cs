using NUnit.Framework;
using EventBackofficeBackend.Controllers;
using EventBackofficeBackend.Models.DTOs.Event;
using EventBackofficeBackend.Repositories;
using EventBackofficeBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Tests
{
    [TestFixture]
    public class EventsControllerTests
    {
        private EventsController _controller = default!;
        private EventBackofficeBackendContext _context = default!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventBackofficeBackendContext>()
                .UseSqlite("Data Source = ~/EBO.db")
                .Options;
            _context = new EventBackofficeBackendContext(options);
            _controller = new EventsController(_context);
        }

        [Test]
        public async Task GetEvents_ReturnsOkObjectResult_WithGetEventsResponse()
        {
            // Arrange
            var venueId = 1;
            var startDate = "2023-03-22";
            var request = new GetEventsRequest { VenueID = venueId, Date = startDate };

            // Act
            var result = await _controller.GetEvents(venueId, startDate) as OkObjectResult;
            var response = result!.Value as GetEventsResponse;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response!.Events.Count);
        }

        [Test]
        public async Task GetEventById_ReturnsOkObjectResult_WithGetSingleEventResponse()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _controller.GetEventById(id) as OkObjectResult;
            var response = result!.Value as GetSingleEventResponse;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(id, response!.EventID);
        }

        [Test]
        public async Task PostEvent_ReturnsCreatedAtActionResult_WithPostEventResponse()
        {
            // Arrange
            var name = "Test Event";
            var startDate = "2023-03-22";
            var endDate = "2023-03-23";
            var request = new PostEventRequest { Name = name, StartDate = startDate, EndDate = endDate };

            // Act
            var result = await _controller.PostEvent(name, startDate, endDate) as CreatedAtActionResult;
            var response = result!.Value as PostEventResponse;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task DeleteEvent_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _controller.DeleteEvent(id) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result!.StatusCode);
        }
    }
}