using DATN_Back_end.Dto.DtoWorkingTime;
using DATN_Back_end.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Repositories
{
    public class WorkingTimeRepository : IWorkingTimeRepository
    {
        private readonly DataContext dataContext;

        private readonly IAuthenticationService authenticationService;

        public WorkingTimeRepository(DataContext dataContext, IAuthenticationService authenticationService)
        {
            this.dataContext = dataContext;
            this.authenticationService = authenticationService;
        }

        public async Task<List<WorkingTimeItem>> GetAllUserWorkingTime(DateTime dateTime)
        {
            var entry = (await dataContext.Timekeepings
                .Where(x => x.CheckinTime.Value.Year == dateTime.Year
                && x.CheckinTime.Value.Month == dateTime.Month)
                .Include(x => x.User)
                .ToListAsync())
                .GroupBy(x => x.UserId)
                .Select(x => x)
                .ToList();

            return entry.Select(x => new WorkingTimeItem()
            {
                Time = x.Sum(c => (c.CheckoutTime.Value - c.CheckinTime.Value).TotalHours),
            })
            .ToList();
        }

        public async Task<List<WorkingTimeItem>> GetUserWorkingTimeByMonth(Guid departmentId, DateTime dateTime)
        {
            var entry = (await dataContext.Timekeepings
                .Include(x => x.User)
                .Where(x => x.CheckinTime.Value.Year == dateTime.Year
                && x.CheckinTime.Value.Month == dateTime.Month
                && x.User.DepartmentId == departmentId)
                .ToListAsync())
                .GroupBy(x => x.UserId)
                .Select(x => x)
                .ToList();

            return entry.Select(x => new WorkingTimeItem(){
                        Time = x.Sum(c => (c.CheckoutTime.Value - c.CheckinTime.Value).TotalHours)})
                    .ToList();
        }

        public async Task<double> GetWorkingTime(DateTime dateTime)
        {
            var currentUserId = authenticationService.CurrentUserId;

            var entry = await dataContext.Timekeepings.Where(x => x.UserId == currentUserId
               && x.CheckinTime.Value.Year == dateTime.Year
               && x.CheckinTime.Value.Month == dateTime.Month).Select(x => x).ToListAsync();
                
            return entry.Sum(x => (x.CheckoutTime.Value - x.CheckinTime.Value).TotalHours);
        }
    }
}
