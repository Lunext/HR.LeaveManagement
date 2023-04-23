using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Shared;

public abstract class BaseLeaveType
{
    public string Name { get; set; } = string.Empty;

    public int DefaultDays { get; set; }
}
