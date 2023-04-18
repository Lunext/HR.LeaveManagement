using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Data.Common;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{

    private readonly ILeaveTypeRepository leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    {

        this.leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {

        //validations 

        //retrieve  domain entity object 

        var leaveTypeToDelete = await leaveTypeRepository.GetByIdAsync(request.Id);


        //verify the record exists
        if(leaveTypeToDelete == null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id); 
        }
      

        //remove to database 
        await leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

        //return record Id
        return Unit.Value;
    }
}
