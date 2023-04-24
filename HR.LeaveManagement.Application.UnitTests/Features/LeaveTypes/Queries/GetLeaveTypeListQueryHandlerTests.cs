using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeListQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> mockRepo;
    private IMapper mapper;
    //private Mock<IAppLogger<GetLeaveTypesQueryHandler>> mockAppLogger;

    public GetLeaveTypeListQueryHandlerTests()
    {
        this.mockRepo = MockLeaveTypeRepository.GetLeaveTypesMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>(); 


        });

        this.mapper = mapperConfig.CreateMapper();

        //this.mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypesQueryHandler( mapper,mockRepo.Object);

        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypesDto>>();

        result.Count.ShouldBe(3); 


    }
}
