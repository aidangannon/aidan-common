namespace Aidan.Common.Core.Interfaces.Excluded
{
    public interface IGenericInitializable<in T>
    {
        public Result Initialize( T parameters );
    }
}