
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> logger;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, 
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    {
        this.mapper = mapper;
        this.leaveTypeRepository = leaveTypeRepository;
        this.logger = logger;
    }
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {

        //Validate incoming data 
        var validator = new UpdateLeaveTypeCommandValidator(leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.Id);
            throw new BadRequestException("Invalid Leavetype", validationResult);
        }

        //Convert to domain entity object 
        var leaveTypeToUpdate = mapper.Map<Domain.LeaveType>(request);

        //add to database
        await leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        // return Unit value
        return Unit.Value; 
    }
}

