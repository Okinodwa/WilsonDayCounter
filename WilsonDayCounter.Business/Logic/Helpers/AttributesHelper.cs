using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WilsonDayCounter.Business.Logic.Helpers
{
    internal static class AttributesHelper
    {
        public static StringLengthAttribute GetStringLengthAttribute(Type t, string propertyName)
        {
            return t.GetProperty(propertyName).GetCustomAttributes<StringLengthAttribute>().FirstOrDefault();
        }
    }
}
