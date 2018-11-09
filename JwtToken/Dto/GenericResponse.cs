using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtToken.Dto
{
    public class GenericResponse<T> where T : class
    {
        public bool sucess { get; set; }
        public string error_message { get; set; }
        public T content { get; set; }
    }
}
