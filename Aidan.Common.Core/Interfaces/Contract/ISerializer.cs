namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface ISerializer
    {
        string Serialize( object content );

        T Deserialize<T>( string content );
    }
}