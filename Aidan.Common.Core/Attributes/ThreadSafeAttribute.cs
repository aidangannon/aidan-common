using System.ComponentModel;

namespace Aidan.Common.Core.Attributes
{
    public class ThreadSafeAttribute : DescriptionAttribute
    {
        public ThreadSafeAttribute( )
        {
            DescriptionValue = "this is thread safe";
        }
    }
}