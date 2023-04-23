using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Shared;

public class BaseLeaveAllocationValidator: AbstractValidator<BaseLeaveAllocation>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public BaseLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        RuleFor(p => p.LeaveTypeId)
           .GreaterThan(0)
           .MustAsync(LeaveTypeMustExist)
           .WithMessage("{PropertyName} does not exist.");
       
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
    {
        var leaveType = await leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}
