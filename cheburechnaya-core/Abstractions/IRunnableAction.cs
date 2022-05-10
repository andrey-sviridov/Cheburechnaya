using MediatR;

namespace cheburechnaya_core.Abstractions {
	public interface IRunnableAction<T> : IRequest<T> { }

}
