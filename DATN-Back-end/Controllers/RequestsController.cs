using DATN_Back_end.Dto.DtoFormRequest;
using DATN_Back_end.Models;
using DATN_Back_end.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Controllers
{
    [Route("api/request")]
    public class RequestsController : ControllerBase
    {
        private IFormRequestRepository formRequestRepository;

        public RequestsController(IFormRequestRepository formRequestRepository)
        {
            this.formRequestRepository = formRequestRepository;
        }

        [HttpGet]
        public async Task<List<FormRequestItem>> Get() => await formRequestRepository.Get();

        [HttpGet("{id}")]
        public async Task<FormRequestDetail> Get(Guid id) => await formRequestRepository.Get(id);

        [HttpPost]
        public async Task Create([FromBody] FormRequestForm requestForm) => await formRequestRepository.Create(requestForm);

        [HttpPut("{id}")]
        public async Task Update(Guid id, [FromBody] FormRequestForm requestForm) => await formRequestRepository.Update(id, requestForm); 
    }
}
