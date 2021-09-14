using System;
using Aidan.Common.Core.Interfaces.Contract;

namespace Pinfluencer.SocialWrangler.Crosscutting.Utils
{
    public class DateTimeAdapter : IDateTimeAdapter
    {
        public DateTime Now( ) { return DateTime.Now; }
    }
}