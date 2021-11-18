using System;
using System.Collections.Generic;
using System.Text;

namespace WilsonDayCounter.Business.Logic
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException() : base() { }

        public InvalidArgumentException(string message) : base(message) { }
    }
}
