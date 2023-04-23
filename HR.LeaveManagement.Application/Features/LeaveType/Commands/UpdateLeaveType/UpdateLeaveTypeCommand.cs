
using HR.LeaveManagement.Application.Features.LeaveType.Shared;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommand: BaseLeaveType, IRequest<Unit>
    {
        public int Id { get; set; } 
      

    }
}
