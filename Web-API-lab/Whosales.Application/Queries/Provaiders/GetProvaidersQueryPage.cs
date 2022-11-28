using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Provaiders
{
	public record GetProvaidersQueryPage(int pageSize, int pageNumber,
		Func<Provaider, dynamic>? orderByRule = null, Func<Provaider, bool>? whereRule = null)
		: IRequest<IQueryable<Provaider>>;
}
