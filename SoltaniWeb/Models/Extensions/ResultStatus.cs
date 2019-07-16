using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Extensions
{
    public class ResultStatus
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsSuccessed { get; set; }
        public MessageType Type { get; set; }
    }
}
