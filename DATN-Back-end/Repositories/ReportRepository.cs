using DATN_Back_end.Dto.DtoReport;
using DATN_Back_end.Exceptions;
using DATN_Back_end.Models;
using DATN_Back_end.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly DataContext dataContext;

        private readonly ICloudinaryService cloudinaryService;

        public ReportRepository(DataContext dataContext,
            ICloudinaryService cloudinaryService) : base(dataContext)
        {
            this.dataContext = dataContext;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task Create(ReportForm reportForm)
        {
            var reportFormDto = new ReportFormDto();

            reportForm.CopyTo(reportFormDto);
            reportFormDto.UploadFileLink = await cloudinaryService.UploadFile(reportForm.File);
            reportFormDto.CreatedTime = DateTime.Now;

            await base.Create(reportFormDto);
        }

        public async Task<List<ReportItem>> Get()
        {
            return await dataContext.Reports
                .Include(x => x.Comments)
                .Select(x => x.ConvertTo<ReportItem>())
                .ToListAsync();
        }

        public async Task<ReportDetail> Get(Guid id)
        {
            var entry = await dataContext.Reports
                .Include(x => x.Comments)
                .Select(x => x.ConvertTo<ReportDetail>())
                .FirstOrDefaultAsync();

            if (entry == null)
            {
                throw new NotFoundException("Report cannot be found");
            }

            return entry;
        }

        public async Task Update(Guid id, ReportForm reportForm)
        {
            var reportFormDto = new ReportFormDto();

            reportForm.CopyTo(reportFormDto);
            reportFormDto.UploadFileLink = await cloudinaryService.UploadFile(reportForm.File);
            reportFormDto.UpdatedTime = DateTime.Now;

            await base.Update(id, reportFormDto);
        }
    }
}
