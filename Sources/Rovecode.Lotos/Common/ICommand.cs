using System;

namespace Rovecode.Lotos.Common
{
    public interface ICommand
    {
        public void Run();
    }

    public interface IArgumentCommand<T1>
    {
        public void Run(T1 arg1);
    }

    public interface IArgumentCommand<T1, T2>
    {
        public void Run(T1 arg1, T2 arg2);
    }

    public interface IResultCommand<R>
    {
        public R Run();
    }

    public interface IResultCommand<T1, R>
    {
        public R Run(T1 arg1);
    }

    public interface IResultCommand<T1, T2, R>
    {
        public R Run(T1 arg1, T2 arg2);
    }
}
