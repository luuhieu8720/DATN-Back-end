using DATN_Back_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Dto.DtoFormRequest
{
    public class FormRequestDetail
    {
        public string Content { get; set; }

        public string Reason { get; set; }

        public int? Hours { get; set; }

        public DateTime SubmitedTime { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public DateTime RequestDate { get; set; }

        public int StatusId { get; set; }

        public FormStatus FormStatus { get; set; }
        public Guid RequestTypeId { get; set; }

        public RequestType RequestType { get; set; }
    }
}
