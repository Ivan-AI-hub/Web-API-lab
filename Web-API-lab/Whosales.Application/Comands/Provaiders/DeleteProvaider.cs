using MediatR;

namespace Whosales.Application.Comands.Provaiders
{
	public record DeleteProvaider(int id) : IRequest;
}
