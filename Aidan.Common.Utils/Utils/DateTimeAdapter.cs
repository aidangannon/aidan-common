using System;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Utils
{
    public class DateTimeAdapter : IDateTimeAdapter
    {
        public DateTime Now( ) { return DateTime.Now; }
    }
}