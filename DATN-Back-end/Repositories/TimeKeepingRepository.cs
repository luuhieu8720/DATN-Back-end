using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATN_Back_end.Dto.DtoTimeKeeping;
using DATN_Back_end.Exceptions;
using DATN_Back_end.Models;
using DATN_Back_end.Services;
using Microsoft.EntityFrameworkCore;

namespace DATN_Back_end.Repositories
{
    public class TimeKeepingRepository : Repository<Timekeeping>, ITimeKeepingRepository
    {
        private readonly DataContext dataContext;

        public TimeKeepingRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Guid> CheckIn(TimeKeepingForm timeKeepingForm)
        {
            var timeKeeping = timeKeepingForm.ConvertTo<Timekeeping>();
            await dataContext.Timekeepings.AddAsync(timeKeeping);

            await dataContext.SaveChangesAsync();

            return timeKeeping.Id;
        }

        public async Task CheckOut(TimeKeepingForm timeKeepingForm)
        {
            var entry = await dataContext.Timekeepings
                .Where(x => x.UserId == timeKeepingForm.UserId && 
                DateTime.Compare(timeKeepingForm.CheckinTime.Value.Date, x.CheckinTime.Value.Date) == 0)
                .FirstOrDefaultAsync();

            timeKeepingForm.CopyTo(entry);

            dataContext.Entry(entry).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }

        public async Task<List<TimeKeepingItem>> GetTimeKeepingInfos(Guid userId)
        {
            return await dataContext.Timekeepings.Where(x => x.UserId == userId)
                .Select(x => x.ConvertTo<TimeKeepingItem>())
                .ToListAsync();
        }

        public async Task<TimeKeepingDetail> ValidateCheckinToday(Guid userId)
        {
            var today = DateTime.Now.Date;
            var entry = await dataContext.Timekeepings.Where(x => x.UserId == userId && DateTime.Compare(today,x.CheckinTime.Value.Date) == 0)
                .FirstOrDefaultAsync();
            var test = await dataContext.Timekeepings.Select(x => x.CheckinTime.Value.Date).FirstOrDefaultAsync();
            var check = DateTime.Compare(test, today);
            if (entry == null)
            {
                throw new NotFoundException("This user didnt checkin today");
            }

            else
            {
                return entry.ConvertTo<TimeKeepingDetail>();
            }
        }
    }
}
