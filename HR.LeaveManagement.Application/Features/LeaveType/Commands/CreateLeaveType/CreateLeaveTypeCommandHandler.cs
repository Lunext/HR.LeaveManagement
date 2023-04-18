using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;


namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper mapper;
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        this.mapper = mapper;
        this.leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate incoming data 

        var validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any()) {
            throw new BadRequestException("Invalid Leavetype", validationResult);
        }


        // Convert to domain entity object 
        var leaveTypeToCreate = mapper.Map<Domain.LeaveType>(request);



        //Add to database 
        await leaveTypeRepository.CreateAsync(leaveTypeToCreate);


        //return record id 
        return leaveTypeToCreate.Id;
        
    }
}
