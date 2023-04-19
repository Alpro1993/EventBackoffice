using NUnit.Framework;
using EventBackoffice.Backend.Data;
using EventBackoffice.Backend.Repositories;
using Moq;
using Microsoft.EntityFrameworkCore;
using EventBackoffice.Backend.Models.DTOs.Event;
using Microsoft.AspNetCore.Mvc;
using EventBackoffice.Backend.Models;

namespace EventBackoffice.Backend.Tests
{
    public class EventsRepositoryTests
    {
        private EventsRepository _repository = default!;
        private Mock<BackendContext> _contextMock = default!;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<BackendContext>
                            (new DbContextOptions<BackendContext>());
            _repository = new EventsRepository {_context = _contextMock.Object};
        }

        [Test]
        public async Task CreateAsync_ValidInput_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<BackendContext>()
                .UseSqlite("Data Source = EBO.db")
                .Options;
            var context = new BackendContext(contextOptions);
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
            var contextOptions = new DbContextOptionsBuilder<BackendContext>()
                .UseSqlite("Data Source = EBO.db")
                .Options;
            var context = new BackendContext(contextOptions);
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