using DATN_Back_end.Dto.DtoTimeKeeping;
using DATN_Back_end.Models;
using DATN_Back_end.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Controllers
{
    [Route("api/timekeeping")]
    public class TimeKeepingsController : ControllerBase
    {
        private readonly IRepository<Timekeeping> repository;

        private readonly ITimeKeepingRepository timeKeepingRepository;

        public TimeKeepingsController(IRepository<Timekeeping> repository,
            ITimeKeepingRepository timeKeepingRepository)
        {
            this.repository = repository;
            this.timeKeepingRepository = timeKeepingRepository;
        }

        [HttpPost]
        public async Task<Guid> Checkin([FromBody] TimeKeepingForm timeKeepingForm) => await timeKeepingRepository.CheckIn(timeKeepingForm);

        [HttpGet]
        public async Task<List<TimeKeepingItem>> Get() => await repository.Get<TimeKeepingItem>();

        [HttpGet("getbyid/{id}")]
        public async Task<TimeKeepingDetail> Get(Guid id) => await repository.Get<TimeKeepingDetail>(id);

        [HttpPut]
        public async Task Checkout([FromBody] TimeKeepingForm timeKeepingForm) => await timeKeepingRepository.CheckOut(timeKeepingForm);

        [HttpGet("{userId}")]
        public async Task<List<TimeKeepingItem>> GetTimeKeepingInfos(Guid userId) => await timeKeepingRepository.GetTimeKeepingInfos(userId);

        [HttpGet("validate")]
        public async Task<TimeKeepingDetail> ValidateCheckinToday(Guid userId) => await timeKeepingRepository.ValidateCheckinToday(userId);
    }
}
