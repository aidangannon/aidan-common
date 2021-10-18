using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface IApiClient<T> : IGenericApiClient<T> where T : ISerializer
    {
    }
}