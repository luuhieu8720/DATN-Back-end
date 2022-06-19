using DATN_Back_end.Dto.DtoWorkingTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Repositories
{
    public interface IWorkingTimeRepository
    {
        Task<List<WorkingTimeItem>> GetUserWorkingTimeByMonth(Guid departmentId, DateTime dateTime);

        Task<List<WorkingTimeItem>> GetAllUserWorkingTime(DateTime dateTime);

        Task<double> GetWorkingTime(DateTime dateTime);
    }
}
