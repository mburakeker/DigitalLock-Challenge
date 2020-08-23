using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLock.Infrastructure.Exceptions
{
    public class ExceptionModel : System.Exception
    {
        public string Title { get; set; }

        public string Detail { get; set; }

        public int StatusCode { set; get; }
    }
}
