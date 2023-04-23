using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Shared;

public class BaseLeaveTypeValidator: AbstractValidator<BaseLeaveType>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public BaseLeaveTypeValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
        .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
        .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");


        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exist");

    }
    private Task<bool> LeaveTypeNameUnique(BaseLeaveType command, CancellationToken token)
    {
        return leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }

}

