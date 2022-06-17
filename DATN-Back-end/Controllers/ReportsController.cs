using DATN_Back_end.Dto.DtoReport;
using DATN_Back_end.Models;
using DATN_Back_end.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Controllers
{
    [Route("api/reports")]
    public class ReportsController
    {
        private readonly IRepository<Report> repository;

        private readonly IReportRepository reportRepository;

        public ReportsController(IRepository<Report> repository,
            IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<List<ReportItem>> Get() => await reportRepository.Get();

        [HttpPost]
        public async Task Create([FromBody]ReportForm reportForm) => await reportRepository.Create(reportForm);

        [HttpGet("{id}")]
        public async Task Get(Guid id) => await reportRepository.Get(id);

        [HttpPut("{id}")]
        public async Task Update(Guid id, [FromBody]ReportForm reportForm) => await reportRepository.Update(id, reportForm);
    }
}
