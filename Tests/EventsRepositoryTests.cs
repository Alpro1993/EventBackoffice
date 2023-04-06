using NUnit.Framework;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Repositories;
using Moq;
using Microsoft.EntityFrameworkCore;
using EventBackofficeBackend.Models.DTOs.Event;
using Microsoft.AspNetCore.Mvc;
using EventBackofficeBackend.Models;

namespace EventBackofficeBackend.Tests
{
    public class EventsRepositoryTests
    {
        private EventsRepository _repository = default!;
        private Mock<EventBackofficeBackendContext> _contextMock = default!;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<EventBackofficeBackendContext>
                            (new DbContextOptions<EventBackofficeBackendContext>());
            _repository = new EventsRepository {_context = _contextMock.Object};
        }

        [Test]
        public async Task CreateAsync_ValidInput_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<EventBackofficeBackendContext>()
                .UseSqlite("Data Source = EBO.db")
                .Options;
            var context = new EventBackofficeBackendContext(contextOptions);
            var repository = new EventsRepository { _context = context };
            var postRequest = new PostEventRequest
            {
                Name = "Test Event",
                StartDate = "01/01/2024",
                EndDate = "02/01/2024"
            };

            // Act
            var result = await repository.CreateAsync(postRequest);

            // Assert
            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
        }

        [Test]
        public async Task CreateAsync_EventNameAlreadyExists_ShouldReturnBadRequestObjectResult()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<EventBackofficeBackendContext>()
                .UseSqlite("Data Source = EBO.db")
                .Options;
            var context = new EventBackofficeBackendContext(contextOptions);
            var repository = new EventsRepository { _context = context };
            var existingEvent = new Event { Name = "Test Event", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) };
            context.Events.Add(existingEvent);
            await context.SaveChangesAsync();
            var postRequest = new PostEventRequest
            {
                Name = "Test Event",
                StartDate = "01/01/2024",
                EndDate = "02/01/2024"
            };

            // Act
            var result = await repository.CreateAsync(postRequest);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}