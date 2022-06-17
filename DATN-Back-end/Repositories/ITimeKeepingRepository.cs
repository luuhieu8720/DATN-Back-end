using DATN_Back_end.Dto.DtoTimeKeeping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Repositories
{
    public interface ITimeKeepingRepository
    {
        Task<Guid> CheckIn(TimeKeepingForm timeKeepingForm);

        Task<List<TimeKeepingItem>> GetTimeKeepingInfos(Guid userId);

        Task<TimeKeepingDetail> ValidateCheckinToday(Guid userId);

        Task CheckOut(TimeKeepingForm timeKeepingForm);
    }
}
