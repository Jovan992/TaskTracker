using CommonUtils.ResultDataResponse;
using NSubstitute;
using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_BL.Services;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL_Tests.Services;

public class ProjectServiceTests
{
    private readonly IProjectRepository projectRepository;
    private readonly IProjectService projectService;
    private readonly string notFoundProjectByIdMessage = "Project with given id not found.";
    private readonly string noProjectsFoundMessage = "No projects found.";
    private readonly int projectId = (new Random()).Next();

    public ProjectServiceTests()
    {
        projectRepository = Substitute.For<IProjectRepository>();
        projectService = new ProjectService(projectRepository);
    }


    [Fact]
    public async Task CreateProject_ShouldReturnCreatedAtActionResultData()
    {
        // Arrange
        var createProjectDto = new CreateProjectDto
        {
            Name = "Test",
            Status = ProjectStatusEnum.NotStarted,
            Priority = 1
        };

        var project = new Project
        {
            ProjectId = projectId,
            Name = "Test",
            Status = ProjectStatusEnum.NotStarted,
            Priority = 1,
        };

        var expectedResultData = new CreatedAtActionResultData<Project>(project);

        projectRepository.CreateProject(Arg.Any<Project>()).Returns(expectedResultData);

        // Act
        var result = await projectService.CreateProject(createProjectDto);

        // Assert
        Assert.IsType<CreatedAtActionResultData<ProjectDto>>(result);
        Assert.Equal(projectId, result.Data!.Id);
    }


    [Fact]
    public async Task GetProjectById_ShouldReturnOkResultData_WhenProjectExists()
    {
        // Arrange
        var projectInDb = new Project
        {
            ProjectId = projectId,
            Name = "Test",
            Status = ProjectStatusEnum.NotStarted,
            Priority = 1,
        };

        var resultDataProject = new OkResultData<Project>(projectInDb);

        projectRepository.GetProjectById(projectId).Returns(resultDataProject);

        // Act
        var result = await projectService.GetProjectById(projectId);

        // Assert
        Assert.IsType<OkResultData<ProjectDto>>(result);
        Assert.Equal(projectId, result.Data!.Id);
    }

    [Fact]
    public async Task GetprojectById_ShouldReturnNotFoundResultData_WhenProjectIsNotFound()
    {
        // Arrange
        var resultDataNotFound = new NotFoundResultData<Project>(notFoundProjectByIdMessage);

        projectRepository.GetProjectById(Arg.Any<int>()).Returns(resultDataNotFound);

        // Act
        var result = await projectService.GetProjectById(projectId);

        // Assert
        Assert.IsType<NotFoundResultData<ProjectDto>>(result);
        Assert.Equal(notFoundProjectByIdMessage, result.Message);
    }

    [Fact]
    public async Task GetProjects_ShouldReturnOkResultData_WhenProjectExists()
    {
        // Arrange
        var parameters = new ProjectParameters();

        var projects = new List<Project>()
        {
            new ()
            {
                ProjectId = projectId,
                Name = "Test1",
                Status = ProjectStatusEnum.NotStarted,
                Priority = 1,
            }, new ()
            {
                ProjectId = projectId+1,
                Name = "Test2",
                Status = ProjectStatusEnum.NotStarted,
                Priority = 2,
            }, new ()
            {
                ProjectId = projectId+2,
                Name = "Test3",
                Status = ProjectStatusEnum.NotStarted,
                Priority = 3,
            },
        };

        var pagedListOfProjects = new PagedList<Project>(projects, projects.Count, parameters.PageNumber, parameters.PageSize);

        var resultDataProject = new OkResultData<PagedList<Project>>(pagedListOfProjects);

        projectRepository.GetProjects(Arg.Any<ProjectParameters>()).Returns(resultDataProject);

        // Act
        var result = await projectService.GetProjects(parameters);

        // Assert
        Assert.IsType<OkResultData<PagedList<ProjectDto>>>(result);
        Assert.NotNull(resultDataProject.Data);
    }

    [Fact]
    public async Task GetProjects_ShouldReturnNotFoundResultData_WhenProjectDoesntExist()
    {
        // Arrange
        var parameters = new ProjectParameters();
        var resultDataProject = new NotFoundResultData<PagedList<Project>>(noProjectsFoundMessage);

        projectRepository.GetProjects(Arg.Any<ProjectParameters>()).Returns(resultDataProject);

        // Act
        var result = await projectService.GetProjects(parameters);

        // Assert
        Assert.IsType<NotFoundResultData<PagedList<ProjectDto>>>(result);
        Assert.Equal(noProjectsFoundMessage, result.Message);
    }

    [Fact]
    public async Task DeleteProject_ShouldReturnNotFoundResultData_WhenProjectDoesntExist()
    {
        //Arrange
        var expectedResultData = new NotFoundResultData<Project>(notFoundProjectByIdMessage);
        projectRepository.DeleteProject(Arg.Any<int>()).Returns(expectedResultData);

        //Act
        var result = await projectRepository.DeleteProject(projectId);

        //Assert
        Assert.IsType<NotFoundResultData<Project>>(result);
        Assert.Equal(notFoundProjectByIdMessage, result.Message);
    }

    [Fact]
    public async Task DeleteProject_ShouldReturnNoContentResultData_WhenProjectExists()
    {
        //Arrange
        var expectedResultData = new NoContentResultData<Project>();

        projectRepository.DeleteProject(Arg.Any<int>()).Returns(expectedResultData);

        //Act
        var result = await projectRepository.DeleteProject(projectId);

        //Assert
        Assert.IsType<NoContentResultData<Project>>(result);
    }

    [Fact]
    public async Task UpdateProject_ShouldReturnNotFoundResultData_WhenProjectDoesntExists()
    {
        //Arrange
        var updateProjectDto = new UpdateProjectDto
        {
            ProjectId = projectId,
            Name = "Test",
            Status = ProjectStatusEnum.NotStarted,
            Priority = 1
        };

        var expectedResultData = new NotFoundResultData<Project>(notFoundProjectByIdMessage);

        projectRepository.UpdateProject(Arg.Any<Project>()).Returns(expectedResultData);

        //Act
        var result = await projectService.UpdateProject(updateProjectDto);

        //Assert
        Assert.IsType<NotFoundResultData<Project>>(result);
        Assert.Equal(notFoundProjectByIdMessage, result.Message);
    }

    [Fact]
    public async Task UpdateProject_ShouldReturnNoContentResultData_WhenProjectExists()
    {
        //Arrange
        var updateProjectDto = new UpdateProjectDto
        {
            ProjectId = projectId,
            Name = "Test",
            Status = ProjectStatusEnum.NotStarted,
            Priority = 1
        };

        var expectedResultData = new NoContentResultData<Project>();

        projectRepository.UpdateProject(Arg.Any<Project>()).Returns(expectedResultData);

        //Act
        var result = await projectService.UpdateProject(updateProjectDto);

        //Assert
        Assert.IsType<NoContentResultData<Project>>(result);
    }
}
