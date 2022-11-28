using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Storages
{
	public record GetStoragesCount(Func<Storage, bool>? whereRule = null) : IRequest<int>;
}
