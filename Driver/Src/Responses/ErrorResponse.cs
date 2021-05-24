using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.Src.Responses
{
   public class ErrorResponse
    {
        public string StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
