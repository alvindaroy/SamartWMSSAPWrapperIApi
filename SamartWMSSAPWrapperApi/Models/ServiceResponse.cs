﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamartWMSSAPWrapperApi.Model
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; } 
        public bool Success { get; set; } = true; 
        public string Message { get; set; } = null;
    }
}
