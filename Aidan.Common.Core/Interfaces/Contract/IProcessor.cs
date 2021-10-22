namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface IProcessor
    {
        Result RunAndWait( string process, string args );
    }
}