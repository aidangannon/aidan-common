namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface ICsvAdapterFactory
    {
        public ICsvAdapter Factory( string filePath );
    }
}