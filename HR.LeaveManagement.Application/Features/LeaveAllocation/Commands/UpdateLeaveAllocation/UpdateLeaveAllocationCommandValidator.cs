using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator: AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly ILeaveAllocationRepository leaveAllocationRepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.leaveAllocationRepository = leaveAllocationRepository;

        Include(new BaseLeaveAllocationValidator(leaveTypeRepository)); 

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must greater than {comparisonValue}");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage(("{PropertyName} must be after {ComparisonValue}"));

       

        RuleFor(p => p.Id).NotNull()
            .MustAsync(LeaveAllocationMustExist)
            .WithMessage("{PropertyName} must be present"); 
    }

    private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken arg2)
    {
        var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(id);
        return leaveAllocation != null; 
            
    }
}
