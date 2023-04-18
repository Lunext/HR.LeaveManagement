using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class LeaveTypeProfile: Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypesDto, LeaveType>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDetailsDto>();  
    }
}
