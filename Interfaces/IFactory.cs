using System.Numerics;

namespace Interfaces
{
    public interface IFactory<out TOut>
    {
        TOut Create();
    }

    public interface IFactory<in T1, out TOut>
    {
        TOut Create(T1 arg1);
    }
    
    public interface IFactory<in T1, in T2, in T3, out TOut>
    {
        TOut Create(T1 arg1, T2 arg2, T3 arg3);
    }
}