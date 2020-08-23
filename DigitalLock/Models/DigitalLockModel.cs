using System;
using System.Collections.Generic;
using System.Text;
using DigitalLock.Infrastructure.Exceptions;

namespace DigitalLock.Models
{
    public class DigitalLockModel
    {
        public int Size { get; set; }
        public string Chars { get; set; }
        public bool IsLocked { get; set; }
        public string Password { get;  set; }
        public string Charset { get; set; }

    }
}
