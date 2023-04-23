
using HR.LeaveManagement.Application.Features.LeaveType.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommand: BaseLeaveType, IRequest<int>
{
    
}
