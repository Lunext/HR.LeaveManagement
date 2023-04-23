using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper mapper;
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IAppLogger<GetLeaveTypeDetailsQueryHandler> logger;

    public GetLeaveTypeDetailsQueryHandler(IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<GetLeaveTypeDetailsQueryHandler> logger)
    {
        this.mapper = mapper;
        this.leaveTypeRepository = leaveTypeRepository;
        this.logger = logger;
    }

    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailQuery request, CancellationToken cancellationToken)
    {
        //Query the database

        var leaveType = await leaveTypeRepository.GetByIdAsync(request.Id);


        //Mapping t
        var data = mapper.Map<LeaveTypeDetailsDto>(leaveType);

        //Returning data 
        logger.LogInformation("Leave types were retrieved successfully");
        return data;
    }
}
