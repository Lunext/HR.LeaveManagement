using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper mapper;
    private readonly ILeaveAllocationRepository leaveAllocationRepository;

    public GetLeaveAllocationListHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    {
        this.mapper = mapper;
        this.leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        //To add Later 
        //-Get records for specific user 
        //-Get allocations per employee

        var leaveAllocations = await
            leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        var allocations = mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        return allocations; 
    }
}

