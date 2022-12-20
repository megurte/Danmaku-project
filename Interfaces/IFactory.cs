using System.Numerics;

namespace Interfaces
{
    public interface IFactory<T>
    {
        T Create(T arg);
    }
    
    public interface IFactory<T1, in T2>
    {
        T1 Create(T1 arg1, T2 arg2);
    }
    
    public interface IFactory<T1, in T2, in T3>
    {
        T1 Create(T1 arg1, T2 arg2, T3 arg3);
    }
    
    public interface IFactory<T1, in T2, in T3, in T4>
    {
        T1 Create(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }
}