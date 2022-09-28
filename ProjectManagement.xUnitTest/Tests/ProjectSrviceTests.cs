using ProjectManagement.Application.Services.Implementations;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Data.Repositories;
using ProjectManagement.Domain.Entities;
using System;
using Xunit;

namespace ProjectManagement.xUnitTest.Tests
{
    public class ProjectSrviceTests : TestBase
    {
        private readonly IProjectService _projectService;
        public ProjectSrviceTests()
        {
            //seed data
            SeedData.Seed(_context, typeof(User), typeof(Project));

            //generate Service
            var projectRepository = new ProjectRepository(_context);
            _projectService = new ProjectService(projectRepository, _mapper);
        }

        [Fact]
        public async void GetAll()
        {
            var res = await _projectService.GetAll(1);

            Assert.Equal(3, res.Count);
        }

        [Fact]
        public async void GetById()
        {
            var res = await _projectService.GetById(2, 1);

            Assert.Equal("Project2", res.Caption);
        }

        [Fact]
        public async void Create()
        {
            var model = new ProjectViewModel
            {
                UserId = 1,
                Caption = "Project4",
                Employer = "Employer4",
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddDays(10),
                Description = "Description Description",
            };

            var res = await _projectService.Create(model);

            Assert.True(res.Status);
        }

        [Fact]
        public async void CreateWithInvalidFields()
        {
            var model = new ProjectViewModel
            {
                UserId = 1,
                Caption = null,
                Employer = null,
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddDays(10),
                Description = "Description Description",
            };

            var res = await _projectService.Create(model);

            Assert.False(res.Status);
            Assert.Contains("The operation failed!", res.Message);
        }

        [Fact]
        public async void Edit()
        {
            var model = new ProjectViewModel
            {
                Id = 1,
                UserId = 1,
                Caption = "Project11",
                Employer = "Employer1",
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddDays(10),
                Description = "Description Description",
            };

            var res = await _projectService.Edit(model);

            Assert.True(res.Status);

            //var editedModel = await _projectService.GetById(1, 1);
            //Assert.Equal("Project11", editedModel.Caption);
        }

    }
}




