using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Storages
{
	public record GetStorageById(int id) : IRequest<Storage?>;
}
