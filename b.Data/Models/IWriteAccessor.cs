using System;
namespace b.Data.Models
{
    public interface IWriteAccessor<TCommand>
    {
        void Execute(TCommand command);
    }
}
