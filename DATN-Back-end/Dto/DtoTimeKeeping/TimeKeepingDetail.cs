﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Dto.DtoTimeKeeping
{
    public class TimeKeepingDetail
    {
        public DateTime? CheckinTime { get; set; }

        public DateTime? CheckoutTime { get; set; }

        public Guid UserId { get; set; }
    }
}