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
    }
}
