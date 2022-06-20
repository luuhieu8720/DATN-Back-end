using DATN_Back_end.Dto.DtoFilter;
using DATN_Back_end.Dto.DtoFormRequest;
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
    public class FormRequestRepository : Repository<FormRequest>, IFormRequestRepository
    {
        private DataContext dataContext;

        public FormRequestRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task Create(FormRequestForm formRequest)
        {
            formRequest.SubmittedTime = DateTime.Now;

            await base.Create(formRequest);
        }

        public async Task<List<FormRequestDetail>> FilterRequest(RequestsFilter requestsFilter)
        {
            return await dataContext.FormRequests
                .Where(x => (requestsFilter.DepartmentId.HasValue ? x.User.DepartmentId == requestsFilter.DepartmentId.Value 
                : x != null))
                .Where(x => requestsFilter.UserId.HasValue ? x.UserId == requestsFilter.UserId.Value
                : x != null)
                .Where(x => requestsFilter.DateTime.HasValue ? 
                (x.SubmittedTime.Day == requestsFilter.DateTime.Value.Day
                 && x.SubmittedTime.Month == requestsFilter.DateTime.Value.Month
                && x.SubmittedTime.Year == requestsFilter.DateTime.Value.Year)
                : x != null)
                .Where(x => requestsFilter.FormStatusId.HasValue ? x.FormStatus.Id == requestsFilter.FormStatusId.Value
                : x != null)
                .Include(x => x.FormStatus)
                .Select(x => x.ConvertTo<FormRequestDetail>())
                .ToListAsync();
        }

        public async Task<FormRequestDetail> Get(Guid id)
        {
            var entry = await dataContext.FormRequests
                .Include(x => x.FormStatus)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (entry == null)
            {
                throw new NotFoundException("This form request cannot be found");
            }

            return entry.ConvertTo<FormRequestDetail>();
        }

        public async Task<List<FormRequestItem>> Get()
        {
            return await dataContext.FormRequests
                .Include(x => x.FormStatus)
                .Select(x => x.ConvertTo<FormRequestItem>())
                .ToListAsync();
        }

        public async Task Update(Guid id, FormRequestForm formRequest)
        {
            formRequest.UpdatedTime = DateTime.Now;

            await base.Update(id, formRequest);
        }

        public async Task<List<FormRequestDetail>> FilterRequestForUser(RequestsFilter requestsFilter)
        {
            return await dataContext.FormRequests
                .Where(x => (requestsFilter.DepartmentId.HasValue ? x.User.DepartmentId == requestsFilter.DepartmentId.Value
                : x != null))
                .Where(x => requestsFilter.UserId.HasValue ? x.UserId == requestsFilter.UserId.Value
                : x != null)
                .Where(x => requestsFilter.DateTime.HasValue ?
                (x.SubmittedTime.Month == requestsFilter.DateTime.Value.Month
                && x.SubmittedTime.Year == requestsFilter.DateTime.Value.Year)
                : x != null)
                .Where(x => requestsFilter.FormStatusId.HasValue ? x.FormStatus.Id == requestsFilter.FormStatusId.Value
                : x != null)
                .Include(x => x.FormStatus)
                .Select(x => x.ConvertTo<FormRequestDetail>())
                .ToListAsync();
        }
    }
}
