using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Mapper;
using ProjectManagement.Data.Context;
using System;

namespace ProjectManagement.xUnitTest
{
    public class TestBase : IDisposable
    {
        protected readonly PMDbContext _context;
        protected readonly IMapper _mapper;

        public TestBase()
        {
            //generate dbcontext
            var options = new DbContextOptionsBuilder<PMDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            _context = new PMDbContext(options);
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context.Database.EnsureCreated();


            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _mapper = mockMapper.CreateMapper();

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
