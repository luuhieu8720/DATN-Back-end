using DATN_Back_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Dto.DtoFormRequest
{
    public class FormRequestItem
    {
        public string Content { get; set; }

        public string Reason { get; set; }

        public DateTime SubmitedTime { get; set; }

        public Guid UserId { get; set; }

        public int StatusId { get; set; }

        public FormStatus FormStatus { get; set; }
    }
}
