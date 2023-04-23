using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    internal class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IEmailSender emailSender;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public CreateLeaveRequestCommandHandler(IEmailSender emailSender, 
            IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository)
        {
            this.emailSender = emailSender;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Request", validationResult);

            //Get requesting employee's id 
            //Check on employee's allocation 
            //if allocations arent' enough, return validation error with message 

            //Create leave request 
            var leaveRequest = mapper.Map<Domain.LeaveRequest>(request);
            await leaveRequestRepository.CreateAsync(leaveRequest);

            //Send confirmation email 
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {request.StartDate:D} " +
                $"to {request.EndDate:D} has been submited successfully.",
                Subject = "Leave Request Submitted"
            };

            await emailSender.SendEmail(email);

            return Unit.Value; 
            
            
        }
    }
}
