﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Models
{
    public class SuccessModelDTO
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
        public object Data { get; set; }
    }
}