﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class Cash
    {
        public int PaymentId { get; set; }
        public decimal Taken { get; set; }
        public decimal Given { get; set; }
    }
}