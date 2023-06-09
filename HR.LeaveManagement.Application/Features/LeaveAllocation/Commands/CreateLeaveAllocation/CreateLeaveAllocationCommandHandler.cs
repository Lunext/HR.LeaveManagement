﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler :
    IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly ILeaveAllocationRepository leaveAllocationRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public CreateLeaveAllocationCommandHandler(IMapper mapper, 
        ILeaveAllocationRepository leaveAllocationRepository, 
        ILeaveTypeRepository leaveTypeRepository)
    {
        this.mapper = mapper;
        this.leaveAllocationRepository = leaveAllocationRepository;
        this.leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalidd Leave Allocation Request", validationResult);
        }

        //Get Leave type for allocations 

        //Get  Employees 

        //Get Period 

        //Assign Allocations 
        var leaveAllocation = mapper.Map<Domain.LeaveAllocation>(request);
        await leaveAllocationRepository.CreateAsync(leaveAllocation);
        return Unit.Value; 
    }
}
