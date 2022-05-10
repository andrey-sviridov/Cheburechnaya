using AutoMapper;
using MediatR;

namespace cheburechnaya_core.Abstractions {
	public abstract class BaseHandler<TRequest, TResponse, TContext> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> {
		private readonly TContext _context;
		private readonly IMapper _mapper;
		protected TContext Context => _context;
		protected IMapper Mapper => _mapper;
		public BaseHandler(TContext context, IMapper mapper) {
			_context = context;
			_mapper = mapper;
		}

		public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
	}
}
