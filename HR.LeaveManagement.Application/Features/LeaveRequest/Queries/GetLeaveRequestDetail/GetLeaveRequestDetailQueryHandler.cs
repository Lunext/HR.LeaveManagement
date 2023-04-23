using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery,
    LeaveRequestDetailsDto>
{
    private readonly IMapper mapper;
    private readonly ILeaveRequestRepository leaveRequestRepository;

    public GetLeaveRequestDetailQueryHandler(
        IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    {
        this.mapper = mapper;
        this.leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = mapper.Map<LeaveRequestDetailsDto>(
            await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
        //Add Employee details as needed 

        return leaveRequest; 
    }
}
