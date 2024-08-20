using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs.Configurations
{
    public class ActionDTO
    {
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }
    }
}
