using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface ICsvAdapterFactory : IFactory
    {
        public ICsvAdapter Factory( string filePath );
    }
}