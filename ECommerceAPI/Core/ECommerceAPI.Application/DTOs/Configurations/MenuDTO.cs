﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs.Configurations
{
    public class MenuDTO
    {
        public string Name { get; set; }
        public List<ActionDTO> Actions { get; set; } = new();
    }
}
