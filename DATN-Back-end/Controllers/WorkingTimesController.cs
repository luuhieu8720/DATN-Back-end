using DATN_Back_end.Dto.DtoWorkingTime;
using DATN_Back_end.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Controllers
{
    [Route("api/workingtime")]
    public class WorkingTimesController : ControllerBase
    {
        private readonly IWorkingTimeRepository workingTimeRepository;

        public WorkingTimesController(IWorkingTimeRepository workingTimeRepository)
        {
            this.workingTimeRepository = workingTimeRepository;
        }

        [HttpGet("{departmentId}")]
        public async Task<List<WorkingTimeItem>> GetUserWorkingTimeByMonth(Guid departmentId, DateTime dateTime)
            => await workingTimeRepository.GetUserWorkingTimeByMonth(departmentId, dateTime);

        [HttpGet("all")]
        public async Task<List<WorkingTimeItem>> GetAllUserWorkingTime(DateTime dateTime)
            => await workingTimeRepository.GetAllUserWorkingTime(dateTime);

        [HttpGet]
        public async Task<double> GetWorkingTime(DateTime dateTime)
            => await workingTimeRepository.GetWorkingTime(dateTime);
    }
}
