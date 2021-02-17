using System;
namespace b.Data.Models
{
	public interface IReadAccessor<TCommand, TResult>
	{
		TResult Execute(TCommand command);
	}
}
