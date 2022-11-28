using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Storages
{
	public record AddStorage(Storage Storage) : IRequest;
}
