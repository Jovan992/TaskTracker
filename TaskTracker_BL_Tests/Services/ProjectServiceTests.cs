using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker_BL.Services;
using TaskTracker_DAL.Interfaces;

namespace TaskTracker_BL_Tests.Services;

public class ProjectServiceTests
{
    private readonly IProjectRepository projectRepository;
    private readonly ProjectService projectService;
    public ProjectServiceTests()
    {
        projectRepository = Substitute.For<IProjectRepository>();
        projectService = new ProjectService(projectRepository);
    }
    [Fact]
    public async Task ProjectService_DeleteProject_ReturnsTrue()
    {
        //Arrange
        int projectId = 1;
        projectRepository.DeleteProject(projectId).Returns(true);

        //Act
        bool result = await projectRepository.DeleteProject(projectId);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ProjectService_UpdateProject_
}
