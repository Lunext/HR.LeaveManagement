using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, List<LeaveTypesDto>>
{
    private readonly IMapper mapper;
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        this.mapper = mapper;
        this.leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<List<LeaveTypesDto>> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {

        //Query the database
        var leaveTypes = await leaveTypeRepository.GetAsync();

        //Convert data objects to DTO objects 

       var  data= mapper.Map<List<LeaveTypesDto>>(leaveTypes);

        //return list of DTO object
        return data; 
    }
}
