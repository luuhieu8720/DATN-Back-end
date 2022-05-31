using DATN_Back_end.Dto.DtoReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Repositories
{
    public interface IReportRepository
    {
        Task<List<ReportItem>> Get();

        Task<ReportDetail> Get(Guid id);

        Task Create(ReportForm reportForm);

        Task Update(Guid id, ReportForm reportForm);
    }
}
