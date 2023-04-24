using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests
{
    public class HrDatabaseContextTests
    {
        private readonly HrDatabaseContext hrDatabaseContext;

        public HrDatabaseContextTests()
        {
            var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            


            hrDatabaseContext=new HrDatabaseContext(dbOptions);
            
        }


        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            //Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            //Act 
            await hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await hrDatabaseContext.SaveChangesAsync();

            //Assert
            leaveType.DateCreated.ShouldNotBeNull();

        }

        [Fact]
        public async void Save_SetDateModifiedValue()
        {
            //Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            //Act 
            await hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await hrDatabaseContext.SaveChangesAsync();

            //Assert
            leaveType.DateModified.ShouldNotBeNull();

        }
    }
}