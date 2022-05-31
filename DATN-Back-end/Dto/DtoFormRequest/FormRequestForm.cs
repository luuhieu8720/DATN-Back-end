using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Dto.DtoFormRequest
{
    public class FormRequestForm
    {
        public string Content { get; set; }

        public string Reason { get; set; }

        public DateTime SubmittedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public Guid UserId { get; set; }

        public int StatusId { get; set; }
    }
}
