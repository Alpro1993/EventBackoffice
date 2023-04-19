using NUnit.Framework;
using EventBackoffice.Backend.Controllers;
using EventBackoffice.Backend.Models.DTOs.Event;
using EventBackoffice.Backend.Repositories;
using EventBackoffice.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using EventBackoffice.Backend.Mappings;
using EventBackoffice.Backend.Models;

namespace EventBackoffice.Backend.Tests
{
    [TestFixture]
    public class EventsControllerTests
    {
        private EventsController _controller = default!;
        private DbContextOptions<BackendContext> _options = default!;
        private IMapper _mapper = default!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mapperConfig = new MapperConfiguration(config => { config.AddProfile(new EventMappingProfile());});
            IMapper mapper = mapperConfig.CreateMapper();
            _mapper = mapper;

        }

        [Test]
        public async Task GetEvents_ReturnsOkObjectResult_WithGetMultipleEventsResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetEvents_ReturnsOkObjectResult_WithGetMultipleEventsResponse))
                .Options;
            using (var _context = new BackendContext(_options))
            {
                _controller = new EventsController(_context, _mapper);
                DbInitializer.Initialize(_context);
                
                // Act
                var result = await _controller.GetEvents(null, null) as OkObjectResult;
                var response = result!.Value as GetMultipleEventsResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(3, response!.Events.Count);
            }
        }

        [Test]
        public async Task GetEvents_ByDate_ReturnsOkObjectResult_WithGetMultipleEventsResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetEvents_ByDate_ReturnsOkObjectResult_WithGetMultipleEventsResponse))
                .Options;
            using (var _context = new BackendContext(_options))
            {
                _controller = new EventsController(_context, _mapper);
                DbInitializer.Initialize(_context);
                // Arrange
                var startDate = "05/06/2023";

                // Act
                var result = await _controller.GetEvents(null, startDate) as OkObjectResult;
                var response = result!.Value as GetMultipleEventsResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response!.Events.Count);
            }
        }

        [Test]
        public async Task GetEvents_ByVenue_ReturnsOkObjectResult_WithGetMultipleEventsResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetEvents_ByVenue_ReturnsOkObjectResult_WithGetMultipleEventsResponse))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new EventsController(_context, _mapper);
                DbInitializer.Initialize(_context);

                // Arrange
                var venueId = 1;

                // Act
                var result = await _controller.GetEvents(venueId, null) as OkObjectResult;
                var response = result!.Value as GetMultipleEventsResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(3, response!.Events.Count);
            }
        }

        [Test]
        public async Task GetEventById_ReturnsOkObjectResult_WithGetSingleEventResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetEventById_ReturnsOkObjectResult_WithGetSingleEventResponse))
                .Options;
            
            using (var _context = new BackendContext(_options))
            {
                _controller = new EventsController(_context, _mapper);
                DbInitializer.Initialize(_context);
                
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
        }

        [Test]
        public async Task GetEventById_ReturnsNoContentResult()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetEventById_ReturnsNoContentResult))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new EventsController(_context, _mapper);
                DbInitializer.Initialize(_context);
                
                // Arrange
                var id = 55;

                // Act
                var result = await _controller.GetEventById(id) as NoContentResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(204, result!.StatusCode);
            }
        }

        [Test]

        public async Task DeleteEvent_ReturnsNoContentResult()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(DeleteEvent_ReturnsNoContentResult))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new EventsController(_context, _mapper);
                DbInitializer.Initialize(_context);

                // Arrange
                var id = 3;

                // Act
                var result = await _controller.DeleteEvent(id) as NoContentResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(204, result!.StatusCode);
            }
        }
        
        [Test]
        public async Task PostEvent_ReturnsCreatedAtActionResult_WithPostEventResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(PostEvent_ReturnsCreatedAtActionResult_WithPostEventResponse))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new EventsController(_context, _mapper);
                DbInitializer.Initialize(_context);
            
                // Arrange
                var name = "Test Event";
                var startDate = "22/03/2023";
                var endDate = "22/03/2023";
                var request = new PostEventRequest { Name = name, StartDate = startDate, EndDate = endDate };

                // Act
                var result = await _controller.PostEvent(name, startDate, endDate) as CreatedAtActionResult;
                var response = result!.Value as PostEventResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(201, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(4, response!.EventID);
            }
        }
    }
}