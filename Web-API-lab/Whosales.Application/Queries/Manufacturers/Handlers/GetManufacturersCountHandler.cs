using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;

namespace Whosales.Application.Queries.Manufacturers.Handlers
{
	internal class GetManufacturersCountHandler : BaseGetHandler<GetManufacturersCount, int>
	{
		public GetManufacturersCountHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<int> Handle(GetManufacturersCount request, CancellationToken cancellationToken)
		{
			var whereRule = request.whereRule;
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.Manufacturers.Where(whereRule).Count());
		}
	}
}
