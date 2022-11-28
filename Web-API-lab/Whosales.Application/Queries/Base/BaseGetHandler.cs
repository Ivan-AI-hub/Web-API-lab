using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Queries.Base
{
	internal abstract class BaseGetHandler<TRequest, TRespounse>
		: IRequestHandler<TRequest, TRespounse>
		where TRequest : IRequest<TRespounse>
	{
		protected IWholesaleContext _context;

		public BaseGetHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public abstract Task<TRespounse> Handle(TRequest request, CancellationToken cancellationToken);
	}
}
