using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Storages
{
	public record UpdateStorage(int id, Storage Storage) : IRequest;
}
