using System;
using System.Collections.Generic;
using System.Text;

namespace WilsonDayCounter.Business.Logic.Validation
{
    internal class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = "";
    }
}
