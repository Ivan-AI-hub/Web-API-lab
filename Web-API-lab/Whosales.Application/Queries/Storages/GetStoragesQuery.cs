using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Storages
{
	public record GetStoragesQuery : IRequest<IQueryable<Storage>>;
}
