using NUnit.Framework;
using EventBackoffice.Backend.Controllers;
using EventBackoffice.Backend.Models.DTOs.Session;
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
    public class SessionsControllerTests
    {
        private SessionsController _controller = default!;
        private DbContextOptions<BackendContext> _options = default!;
        private IMapper _mapper = default!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mapperConfig = new MapperConfiguration(config => { config.AddProfile(new SessionMappingProfile());});
            IMapper mapper = mapperConfig.CreateMapper();
            _mapper = mapper;

        }

        [Test]
        public async Task GetSessions_ReturnsOkObjectResult_WithGetMultipleSessionsResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessions_ReturnsOkObjectResult_WithGetMultipleSessionsResponse))
                .Options;
            using (var _context = new BackendContext(_options))
            {
                _controller = new SessionsController(_context, _mapper);
                DbInitializer.Initialize(_context);
                
                // Act
                var result = await _controller.GetSessions() as OkObjectResult;
                var response = result!.Value as GetMultipleSessionsResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(6, response!.Sessions.Count);
            }
        }

        [Test]
        public async Task GetSessions_ByDate_ReturnsOkObjectResult_WithGetMultipleSessionsResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessions_ByDate_ReturnsOkObjectResult_WithGetMultipleSessionsResponse))
                .Options;
            using (var _context = new BackendContext(_options))
            {
                _controller = new SessionsController(_context, _mapper);
                DbInitializer.Initialize(_context);
                // Arrange
                var startDate = "01/05/2023";

                // Act
                var result = await _controller.GetSessions(StartDate: startDate) as OkObjectResult;
                var response = result!.Value as GetMultipleSessionsResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response!.Sessions.Count);
            }
        }

        [Test]
        public async Task GetSessions_Parentless_ReturnsOkObjectResult_WithGetMultipleEventsResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessions_Parentless_ReturnsOkObjectResult_WithGetMultipleEventsResponse))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new SessionsController(_context, _mapper);
                DbInitializer.Initialize(_context);

                // Act
                var result = await _controller.GetSessions(Parentless: true) as OkObjectResult;
                var response = result!.Value as GetMultipleSessionsResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(3, response!.Sessions.Count);
            }
        }

        [Test]
        public async Task GetSessions_ByParentId_ReturnsOkObjectResult_WithGetMultipleEventsResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessions_ByParentId_ReturnsOkObjectResult_WithGetMultipleEventsResponse))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new SessionsController(_context, _mapper);
                DbInitializer.Initialize(_context);
                // Arrange
                var parentId = 1;
                // Act
                var result = await _controller.GetSessions(ParentId: parentId) as OkObjectResult;
                var response = result!.Value as GetMultipleSessionsResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response!.Sessions.Count);
            }
        }

        [Test]
        public async Task GetSessionById_ReturnsOkObjectResult_WithGetSingleEventResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessionById_ReturnsOkObjectResult_WithGetSingleEventResponse))
                .Options;
            
            using (var _context = new BackendContext(_options))
            {
                _controller = new SessionsController(_context, _mapper);
                DbInitializer.Initialize(_context);
                
                // Arrange
                var id = 1;

                // Act
                var result = await _controller.GetSessionById(id) as OkObjectResult;
                var response = result!.Value as GetSingleSessionResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(id, response!.SessionID);
            }
        }

        [Test]
        public async Task GetSessionById_ReturnsNotFoundResult()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessionById_ReturnsNotFoundResult))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new SessionsController(_context, _mapper);
                DbInitializer.Initialize(_context);
                
                // Arrange
                var id = 55;

                // Act
                var result = await _controller.GetSessionById(id) as NotFoundResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(404, result!.StatusCode);
            }
        }

        [Test]

        public async Task DeleteSession_ReturnsNoContentResult()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(DeleteSession_ReturnsNoContentResult))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new SessionsController(_context, _mapper);
                DbInitializer.Initialize(_context);

                // Arrange
                var id = 3;

                // Act
                var result = await _controller.DeleteSession(id) as NoContentResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(204, result!.StatusCode);
            }
        }

        [Test]
        public async Task DeleteSession_ReturnsNotFoundResult()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(DeleteSession_ReturnsNotFoundResult))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new SessionsController(_context, _mapper);
                DbInitializer.Initialize(_context);

                // Arrange
                var id = 55;

                // Act
                var result = await _controller.DeleteSession(id) as NotFoundResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(404, result!.StatusCode);
            }
        }
        
        [Test]
        public async Task PostSession_ReturnsOkResult_WithPostEventResponse()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(PostSession_ReturnsOkResult_WithPostEventResponse))
                .Options;

            using (var _context = new BackendContext(_options))
            {
                _controller = new SessionsController(_context, _mapper);
                DbInitializer.Initialize(_context);
            
                // Arrange
                var name = "Test Session";
                var startDate = "22/03/2023";
                var endDate = "22/03/2023";
                var request = new PostSessionRequest { Name = name, StartDate = startDate, EndDate = endDate };

                // Act
                var result = await _controller.PostSession(name, startDate, endDate) as OkObjectResult;
                var response = result!.Value as PostSessionResponse;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(response);
                Assert.AreEqual(7, response!.SessionID);
            }
        }
    }
}