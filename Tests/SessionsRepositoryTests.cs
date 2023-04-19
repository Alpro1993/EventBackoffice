using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventBackoffice.Backend.Data;
using EventBackoffice.Backend.Mappings;
using EventBackoffice.Backend.Models.DTOs.Session;
using EventBackoffice.Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EventBackoffice.Backend.Tests
{
    [TestFixture]
    public class SessionsRepositoryTests
    {
        private SessionsRepository _repository = default!;
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
        public async Task GetSessionById_ReturnsSession()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessionById_ReturnsSession))
                .Options;

            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var id = 1;

                // Act
                var result = await _repository.GetSessionById(new GetSingleSessionRequest {ID = id});

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.SessionID);
            }
        }

        [Test]
        public async Task GetSessionById_ThrowsKeyNotFoundException()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessionById_ThrowsKeyNotFoundException))
                .Options;

            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var id = 1;

                // Act
                try
                {
                    var result = await _repository.GetSessionById(new GetSingleSessionRequest {ID = id});
                }
                catch (KeyNotFoundException e)
                {
                    Assert.IsNotNull(e);
                    Assert.IsNotNull(e.Message);
                    Assert.AreEqual("A session with the given ID was not found", e.Message);
                }
            }
        }

        [Test]
        public async Task GetAllSessions_ReturnsListOfSessions()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllSessions_ReturnsListOfSessions))
                .Options;

            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var request = new GetMultipleSessionsRequest();

                // Act
                var result = await _repository.GetSessionsAsync(request);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(6, result!.Count);
            }
        }

        [Test]
        public async Task GetSessions_OnlyParentless_ReturnsListOfSessions()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessions_OnlyParentless_ReturnsListOfSessions))
                .Options;
            
            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var request = new GetMultipleSessionsRequest {onlyParentlessSessions = true};

                // Act
                var result = await _repository.GetSessionsAsync(request);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result!.Count);
            }
        }

        [Test]
        public async Task GetSessions_ByParentID_ReturnsListOfSessions()
        {
           _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessions_ByParentID_ReturnsListOfSessions))
                .Options;
            
            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var request = new GetMultipleSessionsRequest {ParentID = 1};

                // Act
                var result = await _repository.GetSessionsAsync(request);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result!.Count);
            } 
        }
        
        [Test]
        public async Task GetSessions_ByStartDate_ReturnsListOfSessions()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetSessions_ByStartDate_ReturnsListOfSessions))
                .Options;
            
            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var request = new GetMultipleSessionsRequest {StartDate = "01/05/2023"};

                // Act
                var result = await _repository.GetSessionsAsync(request);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result!.Count);
            } 
        }

        [Test]
        public async Task CreateSession_ReturnsSession()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(CreateSession_ReturnsSession))
                .Options;
            
            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var request = new PostSessionRequest {Name = "testSession", StartDate = "01/05/2023", EndDate = "02/05/2023"};

                // Act
                var result = await _repository.CreateAsync(request);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual("testSession", result!.Name);
                Assert.AreEqual(new DateTime(2023,05,01), result!.StartDate);
                Assert.AreEqual(new DateTime(2023,05,02), result!.EndDate);
            } 
        }

        [Test]
        public async Task PatchSession_ReturnsSession()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(PatchSession_ReturnsSession))
                .Options;
            
            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var request = new PatchSessionRequest {ID = 1, Name = "testSession", StartDate = "18/05/2023", EndDate = "18/05/2023"};

                // Act
                var result = await _repository.PatchAsync(request);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual("testSession", result!.Name);
                Assert.AreEqual(new DateTime(2023,05,18), result!.StartDate);
                Assert.AreEqual(new DateTime(2023,05,18), result!.EndDate);
            } 
        }

        [Test]
        public async Task PatchSession_ThrowsKeyNotFoundException()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(PatchSession_ThrowsKeyNotFoundException))
                .Options;
            
            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var request = new PatchSessionRequest {ID = 55, Name = "testSession", StartDate = "18/05/2023", EndDate = "18/05/2023"};

                // Act
                try
                {
                    var result = await _repository.PatchAsync(request);
                }
                catch (KeyNotFoundException e)
                {
                    Assert.IsNotNull(e);
                    Assert.IsNotNull(e.Message);
                    Assert.AreEqual("A session with the given ID was not found", e.Message);
                }
            } 
        }

        [Test]
        public async Task DeleteSession_ReturnsTrue()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(DeleteSession_ReturnsTrue))
                .Options;
            
            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var id = 1;

                // Act
                var result = await _repository.DeleteAsync(id);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            } 
        }

        [Test]
        public async Task DeleteSession_ThrowsKeyNotFoundException()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: nameof(DeleteSession_ThrowsKeyNotFoundException))
                .Options;
            
            using (var context = new BackendContext(_options))
            {
                _repository = new SessionsRepository {_context = context};
                DbInitializer.Initialize(context);

                // Arrange
                var id = 55;

                // Act
                try 
                {
                    var result = await _repository.DeleteAsync(id);
                }
                catch (KeyNotFoundException e)
                {
                                    // Assert
                    Assert.IsNotNull(e);
                    Assert.IsNotNull(e.Message);
                    Assert.AreEqual("A session with the given ID was not found", e.Message);
                }
            } 
        }
    }
}