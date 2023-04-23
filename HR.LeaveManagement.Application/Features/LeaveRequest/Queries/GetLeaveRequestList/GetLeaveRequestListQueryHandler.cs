using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    private readonly IMapper mapper;
    private readonly ILeaveRequestRepository leaveRequestRepository;

    public GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    {
        
        this.mapper = mapper;
        this.leaveRequestRepository = leaveRequestRepository;
    }


    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        //Check if it is logged in employee

        var leaveRequests = await leaveRequestRepository.GetLeaveRequestWithDetails();

        var requests = mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

        //Fill requests with employee information 
        return requests;


        
    }
}
