using MediatR;

namespace Whosales.Application.Comands.Storages
{
	public record DeleteStorage(int id) : IRequest;
}
