using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Storages
{
	public record GetStoragesQueryPage(int pageSize, int pageNumber,
		Func<Storage, dynamic>? orderByRule = null, Func<Storage, bool>? whereRule = null)
		: IRequest<IQueryable<Storage>>;
}
