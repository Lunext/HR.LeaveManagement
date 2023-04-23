
using HR.LeaveManagement.Application.Features.LeaveAllocation.Shared;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand:BaseLeaveAllocation,IRequest<Unit>
{

}
