using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;

namespace Whosales.Application.Queries.Employers.Handlers
{
	internal class GetEmployersCountHandler : BaseGetHandler<GetEmployersCount, int>
	{
		public GetEmployersCountHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<int> Handle(GetEmployersCount request, CancellationToken cancellationToken)
		{
			var whereRule = request.whereRule;
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.Employers.Where(whereRule).Count());
		}
	}
}
